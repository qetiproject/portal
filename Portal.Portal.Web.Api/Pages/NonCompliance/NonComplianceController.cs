using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Portal.Application.ApplicationUsers.Models;
using Portal.Portal.Application.NonComplianceModule.AddNonComplianceCommentFeature;
using Portal.Portal.Application.NonComplianceModule.ChangeNonComplianceStatusFeature;
using Portal.Portal.Application.NonComplianceModule.CreateNonComplianceFeature;
using Portal.Portal.Application.NonComplianceModule.Models.Enums;
using Portal.Portal.Application.NonComplianceModule.RedirectNonComplianceFeature;
using Portal.Portal.Application.NonComplianceModule.Resources;
using Portal.Portal.Application.TimeOffRequestsModule.ChangeReceivedTimeOffRequestStatusFeature;
using Portal.Portal.Application.TimeOffRequestsModule.CreateTimeOffRequestFeature;
using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;
using Portal.Portal.Application.TimeOffRequestsModule.RedirectTimeOffRequestFeature;
using Portal.Portal.Application.TimeOffRequestsModule.Resources;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Common;
using Portal.Portal.Persistence;
using Portal.Portal.Web.Api.Pages.NonCompliance.Models;
using Portal.Portal.Web.Api.Pages.TimeOffRequests.Models;
using Portal.Portal.Web.Api.Resources;
using System.Drawing;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Portal.Portal.Web.Api.Pages.AdminPanel.Models;
using Portal.Portal.Application.AdminPanelModule.CreateGroupFeature;
using System.Globalization;

namespace Portal.Portal.Web.Api.Pages.NonCompliance
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class NonComplianceController : ControllerBase
    {
        private readonly PortalDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IFileUploadService _fileUploadService;

        public NonComplianceController(
            PortalDbContext dbContext,
            UserManager<User> userManager,
            IFileUploadService fileUploadService)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
            this._fileUploadService = fileUploadService;
        }

        [HttpGet]
        public Result<GroupsListResponse> GroupsList([FromQuery] GroupsListRequest request)
        {
            var groups = _dbContext.Groups.Select(g => new GroupsListModel(g.Id, g.Name)).ToList();

            if (!string.IsNullOrEmpty(request.Search))
                groups = groups.Where(g => g.Group.ToLower().Contains(request.Search.ToLower())).ToList();

            return new Result<GroupsListResponse>(new GroupsListResponse(groups));
        }


        [HttpGet]
        public Result<EmployeesResponse> Employees([FromQuery] EmployeesRequest request)
        {
            if (!User.Identity.IsAuthenticated)
                return new Result<EmployeesResponse>(ErrorResources.Unauthorized);

            var violator = _dbContext.NonCompliances.FirstOrDefault(nc => nc.Id == Convert.ToInt32(request.NonComplianceId))?.Violator;

            var user = _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value).Result;

            var employeeEmails = _dbContext.Employees.Where(e => e.PersonalInformation.Email != user.Email && e.PersonalInformation.Email != violator).Select(e => e.PersonalInformation.Email).ToList();
            
            if (!string.IsNullOrEmpty(request.Search))
                employeeEmails = employeeEmails.Where(e => e.ToLower().Contains(request.Search.ToLower())).ToList();

            return new Result<EmployeesResponse>(new EmployeesResponse(employeeEmails));
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> CreateNonCompliance([FromForm] CreateNonComplianceRequest request)
        {
            if (!User.Identity.IsAuthenticated)
                return new Result<IEntity[]>(ErrorResources.Unauthorized);

            var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);

            var file = await _fileUploadService.UploadFileAsync(request.File);

            var violator = await _dbContext.Employees.Include(e => e.PersonalInformation).FirstOrDefaultAsync(e => e.PersonalInformation.Email == request.Violator);

            var action = new CreateNonComplianceAction(
                request.Title,
                user.Email,
                request.Violator,
                string.IsNullOrEmpty(request.ApprovalDeadline) || request.ApprovalDeadline == "null" ? (DateTime?)null : DateTime.Parse(request.ApprovalDeadline, CultureInfo.InvariantCulture),
                request.Fine,
                file,
                request.Description
            );

            var smsService = new SmsService();

            //smsService.SendSms(
            //    violator.PersonalInformation.PhoneNumber, 
            //    "Tqven mogividat dargveva organizaciashi, sheamowmet “portall” an mimartet administracias");

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new CreateNonComplianceBehavior()
                        ).Save(entityStore, action);
        }

        [HttpGet]
        public Result<AllNonCompliancesResponse> AllNonCompliances([FromQuery] AllNonCompliancesRequest request)
        {
            var employees = _dbContext.Employees.Include(e => e.PersonalInformation).ToList();

            var nonCompliances = _dbContext.NonCompliances
               .OrderByDescending(n => n.Id)
               .Select(nc => new AllNonCompliancesModel(
                   nc.Id.ToString(),
                   nc.Title,
                   nc.Status,
                   nc.CreateDate,
                   GetFullNameByEmail(employees, nc.Sender),
                   GetFullNameByEmail(employees, nc.Violator),
                   new NonComplianceContentModel(
                       new NonComplianceFileModel(nc.File.Name, nc.File.Path, nc.File.Type),
                       string.IsNullOrEmpty(nc.Description) ? string.Empty : TimeOffRequestResources.Description,
                       nc.Description)))
               .ToList();

            if (!string.IsNullOrEmpty(request.Search))
            {
                nonCompliances = nonCompliances
                    .Where(n => n.Number.ToString().ToUpper().Contains(request.Search.ToUpper())
                                || n.Status.ToString().ToUpper().Contains(request.Search.ToUpper())
                                || (n.Sender != null && n.Sender.ToUpper().Contains(request.Search.ToUpper()))
                                || (n.Violator != null && n.Violator.ToUpper().Contains(request.Search.ToUpper()))
                                || (n.Content.Description != null && n.Content.Description.ToUpper().Contains(request.Search.ToUpper()))
                                || n.Group.ToUpper().Contains(request.Search.ToUpper()))
                    .ToList();
            }

            return new Result<AllNonCompliancesResponse>(new AllNonCompliancesResponse(nonCompliances.Count, nonCompliances));
        }

        [HttpGet]
        public Result<ReceivedNonCompliancesResponse> ReceivedNonCompliances([FromQuery] ReceivedNonCompliancesRequest request)
        {
            var user = GetUser();
            if (user == null)
                return new Result<ReceivedNonCompliancesResponse>(ErrorResources.Unauthorized);

            var employees = _dbContext.Employees.Include(e => e.PersonalInformation).ToList();

            var nonCompliances = _dbContext.NonCompliances
                .Where(n => n.Redirects.Any(r => r.Receiver == user.Email))
                .OrderByDescending(n => n.Id)
                .Select(nc => new ReceivedNonCompliancesModel(
                    nc.Id.ToString(),
                    nc.Title,
                    nc.Status,
                    nc.CreateDate,
                    GetFullNameByEmail(employees, nc.Sender),
                    GetFullNameByEmail(employees, nc.Violator),
                    new NonComplianceContentModel(
                        new NonComplianceFileModel(nc.File.Name, nc.File.Path, nc.File.Type),
                        string.IsNullOrEmpty(nc.Description) ? string.Empty : TimeOffRequestResources.Description,
                        nc.Description),
                    nc.Redirects.Any(r => r.Receiver == user.Email && r.StatusChange))
                )
                .ToList();

            if (!string.IsNullOrEmpty(request.Search))
            {
                nonCompliances = nonCompliances
                    .Where(n => n.Number.ToString().ToUpper().Contains(request.Search.ToUpper())
                                || n.Status.ToString().ToUpper().Contains(request.Search.ToUpper())
                                || n.Sender.ToUpper().Contains(request.Search.ToUpper())
                                || n.Violator.ToUpper().Contains(request.Search.ToUpper())
                                || (n.Content.Description != null && n.Content.Description.ToUpper().Contains(request.Search.ToUpper()))
                                || n.Group.ToUpper().Contains(request.Search.ToUpper()))
                    .ToList();
            }

            return new Result<ReceivedNonCompliancesResponse>(new ReceivedNonCompliancesResponse(nonCompliances.Count, nonCompliances));
        }

        [HttpGet]
        public Result<SentNonCompliancesResponse> SentNonCompliances([FromQuery] SentNonCompliancesRequest request)
        {
            var user = GetUser();
            if (user == null)
                return new Result<SentNonCompliancesResponse>(ErrorResources.Unauthorized);

            var employees = _dbContext.Employees.Include(e => e.PersonalInformation).ToList();

            var nonCompliances = _dbContext.NonCompliances
                .Where(t => t.Sender == user.Email)
                .OrderByDescending(n => n.Id)
                .Select(nc => new SentNonCompliancesModel(
                    nc.Id.ToString(),
                    nc.Title,
                    nc.Status,
                    nc.CreateDate,
                    GetFullNameByEmail(employees, nc.Violator),
                    new NonComplianceContentModel(
                        new NonComplianceFileModel(nc.File.Name, nc.File.Path, nc.File.Type),
                        string.IsNullOrEmpty(nc.Description) ? string.Empty : TimeOffRequestResources.Description,
                        nc.Description))
                )
                .ToList();

            if (!string.IsNullOrEmpty(request.Search))
            {
                nonCompliances = nonCompliances
                    .Where(n => n.Number.ToString().ToUpper().Contains(request.Search.ToUpper())
                                || n.Status.ToString().ToUpper().Contains(request.Search.ToUpper())
                                || n.Violator.ToUpper().Contains(request.Search.ToUpper())
                                || (n.Content.Description != null && n.Content.Description.ToUpper().Contains(request.Search.ToUpper()))
                                || n.Group.ToUpper().Contains(request.Search.ToUpper()))
                    .ToList();
            }

            return new Result<SentNonCompliancesResponse>(new SentNonCompliancesResponse(nonCompliances.Count, nonCompliances));
        }

        [HttpGet]
        public Result<MyNonCompliancesResponse> MyNonCompliances([FromQuery] MyNonCompliancesRequest request)
        {
            var user = GetUser();
            if (user == null)
                return new Result<MyNonCompliancesResponse>(ErrorResources.Unauthorized);

            var employees = _dbContext.Employees.Include(e => e.PersonalInformation).ToList();

            var nonCompliances = _dbContext.NonCompliances
                .Where(t => t.Violator == user.Email)
                .OrderByDescending(n => n.Id)
                .Select(nc => new MyNonCompliancesModel(
                    nc.Id.ToString(),
                    nc.Title,
                    nc.Status,
                    nc.CreateDate,
                    GetFullNameByEmail(employees, nc.Sender),
                    new NonComplianceContentModel(
                        new NonComplianceFileModel(nc.File.Name, nc.File.Path, nc.File.Type),
                        string.IsNullOrEmpty(nc.Description) ? string.Empty : TimeOffRequestResources.Description,
                        nc.Description))
                )
                .ToList();

            if (!string.IsNullOrEmpty(request.Search))
            {
                nonCompliances = nonCompliances
                    .Where(n => n.Number.ToString().ToUpper().Contains(request.Search.ToUpper())
                                || n.Status.ToString().ToUpper().Contains(request.Search.ToUpper())
                                || n.Sender.ToUpper().Contains(request.Search.ToUpper())
                                || (n.Content.Description != null && n.Content.Description.ToUpper().Contains(request.Search.ToUpper()))
                                || n.Group.ToUpper().Contains(request.Search.ToUpper()))
                    .ToList();
            }

            return new Result<MyNonCompliancesResponse>(new MyNonCompliancesResponse(nonCompliances.Count, nonCompliances));
        }

        [HttpGet]
        public Result<AllNonComplianceDetailsResponse> AllNonComplianceDetails([FromQuery] AllNonComplianceDetailsRequest request)
        {
            var employees = _dbContext.Employees.Include(e => e.PersonalInformation).ToList();

            var timeOffRequest = (
                    from nc in _dbContext.NonCompliances
                    where nc.Id.ToString() == request.Number
                    select new AllNonComplianceDetailsResponse(
                        nc.Id.ToString(),
                        nc.Title,
                        GetFullNameByEmail(employees, nc.Sender),
                        GetFullNameByEmail(employees, nc.Violator),
                        nc.ApprovalDeadline,
                        nc.CreateDate,
                        nc.Fine,
                        new NonComplianceFileModel(nc.File.Name, nc.File.Path, nc.File.Type),
                        nc.Description,
                        nc.ViolatorComment,
                        new NonComplianceFileModel(nc.ViolatorFile.Name, nc.ViolatorFile.Path, nc.ViolatorFile.Type),
                        nc.Status,
                        GetStatusChangerFullName(employees, nc.StatusChanger, nc.Status),
                        nc.StatusComment,
                        nc.StatusChangeDate,
                        new NonComplianceFileModel(nc.StatusFile.Name, nc.StatusFile.Path, nc.StatusFile.Type),
                        nc.Redirects.OrderBy(r => r.Id).Select(r => new NonComplianceRedirectModel(
                            GetFullNameByEmail(employees, r.Redirecter),
                            GetFullNameByEmail(employees, r.Receiver),
                            r.Comment,
                            new NonComplianceFileModel(r.File.Name, r.File.Path, r.File.Type),
                            r.File.Path != null)).ToList()))
                        .FirstOrDefault();

            return new Result<AllNonComplianceDetailsResponse>(timeOffRequest);
        }

        [HttpGet]
        public Result<SentNonComplianceDetailsResponse> SentNonComplianceDetails([FromQuery] SentNonComplianceDetailsRequest request)
        {
            var employees = _dbContext.Employees.Include(e => e.PersonalInformation).ToList();

            var timeOffRequest = (
                    from nc in _dbContext.NonCompliances
                    where nc.Id.ToString() == request.Number
                    select new SentNonComplianceDetailsResponse(
                        nc.Id.ToString(),
                        nc.Title,
                        GetFullNameByEmail(employees, nc.Violator),
                        nc.ApprovalDeadline,
                        nc.CreateDate,
                        nc.Fine,
                        new NonComplianceFileModel(nc.File.Name, nc.File.Path, nc.File.Type),
                        nc.Description,
                        nc.ViolatorComment,
                        new NonComplianceFileModel(nc.ViolatorFile.Name, nc.ViolatorFile.Path, nc.ViolatorFile.Type),
                        nc.Status,
                        GetStatusChangerFullName(employees, nc.StatusChanger, nc.Status),
                        nc.StatusComment,
                        nc.StatusChangeDate,
                        new NonComplianceFileModel(nc.StatusFile.Name, nc.StatusFile.Path, nc.StatusFile.Type),
                        nc.Redirects.OrderBy(r => r.Id).Select(r => new NonComplianceRedirectModel(
                            GetFullNameByEmail(employees, r.Redirecter),
                            GetFullNameByEmail(employees, r.Receiver),
                            r.Comment,
                            new NonComplianceFileModel(r.File.Name, r.File.Path, r.File.Type),
                            r.File.Path != null)).ToList()))
                        .FirstOrDefault();

            return new Result<SentNonComplianceDetailsResponse>(timeOffRequest);
        }

        [HttpGet]
        public Result<ReceivedNonComplianceDetailsResponse> ReceivedNonComplianceDetails([FromQuery] ReceivedNonComplianceDetailsRequest request)
        {
            var user = GetUser();
            if (user == null)
                return new Result<ReceivedNonComplianceDetailsResponse>(ErrorResources.Unauthorized);

            var employees = _dbContext.Employees.Include(e => e.PersonalInformation).ToList();

            var timeOffRequest = (
                    from nc in _dbContext.NonCompliances
                    where nc.Id.ToString() == request.Number
                    select new ReceivedNonComplianceDetailsResponse(
                        nc.Id.ToString(),
                        nc.Title,
                        GetFullNameByEmail(employees, nc.Sender),
                        GetFullNameByEmail(employees, nc.Violator),
                        nc.ApprovalDeadline,
                        nc.CreateDate,
                        nc.Fine,
                        new NonComplianceFileModel(nc.File.Name, nc.File.Path, nc.File.Type),
                        nc.Description,
                        nc.ViolatorComment,
                        new NonComplianceFileModel(nc.ViolatorFile.Name, nc.ViolatorFile.Path, nc.ViolatorFile.Type),
                        nc.Status,
                        GetStatusChangerFullName(employees, nc.StatusChanger, nc.Status),
                        nc.StatusComment,
                        nc.StatusChangeDate,
                        new NonComplianceFileModel(nc.StatusFile.Name, nc.StatusFile.Path, nc.StatusFile.Type),
                        nc.Redirects.OrderBy(r => r.Id).Select(r => new NonComplianceRedirectModel(
                            GetFullNameByEmail(employees, r.Redirecter),
                            GetFullNameByEmail(employees, r.Receiver),
                            r.Comment,
                            new NonComplianceFileModel(r.File.Name, r.File.Path, r.File.Type),
                            r.File.Path != null)).ToList(),
                        nc.Redirects.Any(r => r.Receiver == user.Email && r.StatusChange)))
                        .FirstOrDefault();

            return new Result<ReceivedNonComplianceDetailsResponse>(timeOffRequest);
        }

        [HttpGet]
        public Result<MyNonComplianceDetailsResponse> MyNonComplianceDetails([FromQuery] MyNonComplianceDetailsRequest request)
        {
            var employees = _dbContext.Employees.Include(e => e.PersonalInformation).ToList();

            var timeOffRequest = (
                    from nc in _dbContext.NonCompliances
                    join senderEmp in _dbContext.Employees on nc.Sender equals senderEmp.PersonalInformation.Email
                    join violatorEmp in _dbContext.Employees on nc.Violator equals violatorEmp.PersonalInformation.Email
                    where nc.Id.ToString() == request.Number
                    select new MyNonComplianceDetailsResponse(
                        nc.Id.ToString(),
                        nc.Title,
                        GetFullNameByEmail(employees, nc.Sender),
                        nc.ApprovalDeadline,
                        nc.CreateDate,
                        nc.Fine,
                        new NonComplianceFileModel(nc.File.Name, nc.File.Path, nc.File.Type),
                        nc.Description,
                        nc.ViolatorComment,
                        new NonComplianceFileModel(nc.ViolatorFile.Name, nc.ViolatorFile.Path, nc.ViolatorFile.Type),
                        nc.Status,
                        GetStatusChangerFullName(employees, nc.StatusChanger, nc.Status),
                        nc.StatusComment,
                        nc.StatusChangeDate,
                        new NonComplianceFileModel(nc.StatusFile.Name, nc.StatusFile.Path, nc.StatusFile.Type),
                        nc.Redirects.OrderBy(r => r.Id).Select(r => new NonComplianceRedirectModel(
                            GetFullNameByEmail(employees, r.Redirecter),
                            GetFullNameByEmail(employees, r.Receiver),
                            r.Comment,
                            new NonComplianceFileModel(r.File.Name, r.File.Path, r.File.Type),
                            r.File.Path != null)).ToList()))
                        .FirstOrDefault();

            return new Result<MyNonComplianceDetailsResponse>(timeOffRequest);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> RedirectNonCompliance([FromForm] RedirectNonComplianceRequest request)
        {
            if (!User.Identity.IsAuthenticated)
                return new Result<IEntity[]>(ErrorResources.Unauthorized);

            var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);

            var file = await _fileUploadService.UploadFileAsync(request.File);

            var action = new RedirectNonComplianceAction(
                Convert.ToInt32(request.Number),
                user.Email,
                request.Receiver,
                file,
                request.Comment,
                request.StatusChange
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new RedirectNonComplianceBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> ChangeNonComplianceStatus([FromForm] ChangeNonComplianceStatusRequest request)
        {
            if (!User.Identity.IsAuthenticated)
                return new Result<IEntity[]>(ErrorResources.Unauthorized);

            var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);

            var file = await _fileUploadService.UploadFileAsync(request.File);

            var action = new ChangeNonComplianceStatusAction(
                Convert.ToInt32(request.Number),
                user.Email,
                request.Status,
                request.Comment,
                file
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new ChangeNonComplianceStatusBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> AddNonComplianceComment([FromForm] AddNonComplianceCommentRequest request)
        {
            if (!User.Identity.IsAuthenticated)
                return new Result<IEntity[]>(ErrorResources.Unauthorized);

            var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);

            var file = await _fileUploadService.UploadFileAsync(request.File);

            var action = new AddNonComplianceCommentAction(
                Convert.ToInt32(request.Number),
                request.Comment,
                file
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new AddNonComplianceCommentBehavior()
                        ).Save(entityStore, action);
        }

        private User GetUser()
        {
            if (!User.Identity.IsAuthenticated)
                return null;

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            return _userManager.FindByEmailAsync(userEmail).Result;
        }

        private static string GetFullNameByEmail(List<Application.EmployeeServiceModule.Models.Employee> employees, string email)
        {
            return employees
                .Where(e => e.PersonalInformation.Email == email)
                .Select(e => e.PersonalInformation.FullName)
                .FirstOrDefault();
        }

        private static string GetStatusChangerFullName(List<Application.EmployeeServiceModule.Models.Employee> employees, string email, NonComplianceStatus status)
        {
            if (status == NonComplianceStatus.InProgress)
            {
                return null;
            }

            var statusChanger = employees
                .Where(e => e.PersonalInformation.Email == email)
                .Select(e => e.PersonalInformation.FullName)
                .FirstOrDefault();

            return string.Format("{0} {1}", statusChanger, NonComplianceResources.ChangedStatus);
        }
    }
}
