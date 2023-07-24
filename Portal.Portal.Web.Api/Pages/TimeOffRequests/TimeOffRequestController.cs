using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Portal.Portal.Application.ApplicationUsers.Models;
using Portal.Portal.Application.EmployeeServiceModule.EditEmployeeJobInfoFeature;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.TimeOffRequestsModule.ChangeReceivedTimeOffRequestStatusFeature;
using Portal.Portal.Application.TimeOffRequestsModule.CreateTimeOffRequestFeature;
using Portal.Portal.Application.TimeOffRequestsModule.DeleteTimeOffRequestFeature;
using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;
using Portal.Portal.Application.TimeOffRequestsModule.RedirectTimeOffRequestFeature;
using Portal.Portal.Application.TimeOffRequestsModule.Resources;
using Portal.Portal.Common;
using Portal.Portal.Persistence;
using Portal.Portal.Web.Api.Pages.Employee.Models;
using Portal.Portal.Web.Api.Pages.EmployeeProfile.Models;
using Portal.Portal.Web.Api.Pages.TimeOffRequests.Models;
using Portal.Portal.Web.Api.Resources;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Portal.Portal.Web.Api.Pages.TimeOffRequests
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class TimeOffRequestController : ControllerBase
    {
        private readonly PortalDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IFileUploadService _fileUploadService;

        public TimeOffRequestController(
            PortalDbContext dbContext,
            UserManager<User> userManager,
            IFileUploadService fileUploadService)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
            this._fileUploadService = fileUploadService;
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> CreateTimeOffRequest([FromForm] CreateTimeOffRequestRequest request)
        {
            if (!User.Identity.IsAuthenticated)
                return new Result<IEntity[]>(ErrorResources.Unauthorized);

            var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);

            var file = await _fileUploadService.UploadFileAsync(request.File);

            var receiver = request.Receiver;
            if (request.Recipient == Recipient.HR)
            {
                receiver = _dbContext.TimeOffRequestPanel.FirstOrDefault()?.HRWhoReceivesTimeOffRequests;
            }
            else if (request.Recipient == Recipient.Supervisor)
            {
                receiver = _dbContext.Employees
                    .Include(emp => emp.PersonalInformation)
                    .Include(emp => emp.EmploymentHistory)
                    .FirstOrDefault(emp => emp.PersonalInformation.Email == user.Email).EmploymentHistory?.Supervisor;
            }

            var action = new CreateTimeOffRequestAction(
                request.Type,
                request.Title,
                user.Email,
                receiver,
                string.IsNullOrEmpty(request.DeadLine) || request.DeadLine == "null" ? (DateTime?)null : DateTime.Parse(request.DeadLine, CultureInfo.InvariantCulture),
                string.IsNullOrEmpty(request.From) || request.From == "null" ? (DateTime?)null : DateTime.Parse(request.From, CultureInfo.InvariantCulture),
                string.IsNullOrEmpty(request.Including) || request.Including == "null" ? (DateTime?)null : DateTime.Parse(request.Including, CultureInfo.InvariantCulture),
                file,
                request.Description
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new CreateTimeOffRequestBehavior()
                        ).Save(entityStore, action);
        }

        [HttpGet]
        public Result<GetRecipientsResponse> GetRecipients()
        {
            if (!User.Identity.IsAuthenticated)
                return new Result<GetRecipientsResponse>(ErrorResources.Unauthorized);

            var user = _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value).Result;

            var hasHR = !string.IsNullOrEmpty(_dbContext.TimeOffRequestPanel.FirstOrDefault()?.HRWhoReceivesTimeOffRequests);

            var hasSupervisor = !string.IsNullOrEmpty(_dbContext.Employees
                                .Include(emp => emp.PersonalInformation)
                                .Include(emp => emp.EmploymentHistory)
                                .FirstOrDefault(emp => emp.PersonalInformation.Email == user.Email).EmploymentHistory?.Supervisor);

            return new Result<GetRecipientsResponse>(new GetRecipientsResponse(hasHR, hasSupervisor));
        }

        [HttpGet]
        public Result<EmployeesResponse> Employees([FromQuery] EmployeesRequest request)
        {
            if (!User.Identity.IsAuthenticated)
                return new Result<EmployeesResponse>(ErrorResources.Unauthorized);

            var user = _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value).Result;

            var employeeEmails = _dbContext.Employees.Where(e => e.PersonalInformation.Email != user.Email).Select(e => e.PersonalInformation.Email).ToList();

            if (!string.IsNullOrEmpty(request.Search))
                employeeEmails = employeeEmails.Where(e => e.ToLower().Contains(request.Search.ToLower())).ToList();

            return new Result<EmployeesResponse>(new EmployeesResponse(employeeEmails));
        }

        [HttpGet]
        public Result<AllTimeOffRequestsResponse> AllTimeOffRequests([FromQuery] AllTimeOffRequestsRequest request)
        {
            var timeOffRequests = _dbContext.TimeOffRequests
                .OrderByDescending(t => t.Id)
                .Join(_dbContext.Employees,
                      t => t.Sender,
                      e => e.PersonalInformation.Email,
                      (tof, emp) => new { TimeOffRequest = tof, Sender = emp.PersonalInformation.FullName })
                .Join(_dbContext.Employees,
                      t => t.TimeOffRequest.Receiver,
                      e => e.PersonalInformation.Email,
                      (tof, emp) => new { tof.TimeOffRequest, tof.Sender, Receiver = emp.PersonalInformation.FullName })
                .Select(x => new AllTimeOffRequestsModel(
                    x.TimeOffRequest.Id.ToString(),
                    x.TimeOffRequest.Status,
                    x.TimeOffRequest.CreateDate,
                    x.Sender,
                    x.Receiver,
                    x.TimeOffRequest.Title,
                    new ContentModel(
                        new TimeOffRequestFileModel(x.TimeOffRequest.File.Name, x.TimeOffRequest.File.Path, x.TimeOffRequest.File.Type),
                        string.IsNullOrEmpty(x.TimeOffRequest.Description) ? string.Empty : TimeOffRequestResources.Description,
                        x.TimeOffRequest.Description))
                )
                .ToList();

            if (!string.IsNullOrEmpty(request.Search))
            {
                timeOffRequests = timeOffRequests
                    .Where(t => t.Number.ToString().ToUpper().Contains(request.Search.ToUpper())
                                || t.Status.ToString().ToUpper().Contains(request.Search.ToUpper())
                                || t.Sender.ToUpper().Contains(request.Search.ToUpper())
                                || t.Receiver.ToUpper().Contains(request.Search.ToUpper())
                                || (t.Content.Description != null && t.Content.Description.ToUpper().Contains(request.Search.ToUpper()))
                                || t.Title.ToUpper().Contains(request.Search.ToUpper())).ToList();
            }

            return new Result<AllTimeOffRequestsResponse>(new AllTimeOffRequestsResponse(timeOffRequests.Count(), timeOffRequests));
        }

        [HttpGet]
        public Result<SentTimeOffRequestsResponse> SentTimeOffRequests([FromQuery] SentTimeOffRequestsRequest request)
        {
            if (!User.Identity.IsAuthenticated)
                return new Result<SentTimeOffRequestsResponse>(ErrorResources.Unauthorized);

            var user = _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value).Result;

            var timeOffRequests = _dbContext.TimeOffRequests
                .Where(t => t.Sender == user.Email)
                .OrderByDescending(t => t.Id)
                .Join(_dbContext.Employees,
                      t => t.Receiver,
                      e => e.PersonalInformation.Email,
                      (tof, emp) => new { TimeOffRequest = tof, Receiver = emp.PersonalInformation.FullName })
                .Select(x => new SentTimeOffRequestsModel(
                    x.TimeOffRequest.Id.ToString(),
                    x.TimeOffRequest.Status,
                    x.TimeOffRequest.CreateDate,
                    x.Receiver,
                    x.TimeOffRequest.Title,
                    new ContentModel(
                        new TimeOffRequestFileModel(x.TimeOffRequest.File.Name, x.TimeOffRequest.File.Path, x.TimeOffRequest.File.Type),
                        string.IsNullOrEmpty(x.TimeOffRequest.Description) ? string.Empty : TimeOffRequestResources.Description,
                        x.TimeOffRequest.Description))
                )
                .ToList();

            if (!string.IsNullOrEmpty(request.Search))
            {
                timeOffRequests = timeOffRequests
                    .Where(t => t.Number.ToString().ToUpper().Contains(request.Search.ToUpper())
                                || t.Status.ToString().ToUpper().Contains(request.Search.ToUpper())
                                || t.Receiver.ToUpper().Contains(request.Search.ToUpper())
                                || (t.Content.Description != null && t.Content.Description.ToUpper().Contains(request.Search.ToUpper()))
                                || t.Title.ToUpper().Contains(request.Search.ToUpper())).ToList();
            }

            return new Result<SentTimeOffRequestsResponse>(new SentTimeOffRequestsResponse(timeOffRequests.Count(), timeOffRequests));
        }

        [HttpGet]
        public Result<ReceivedTimeOffRequestsResponse> ReceivedTimeOffRequests([FromQuery] ReceivedTimeOffRequestsRequest request)
        {
            if (!User.Identity.IsAuthenticated)
                return new Result<ReceivedTimeOffRequestsResponse>(ErrorResources.Unauthorized);

            var user = _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value).Result;

            var timeOffRequests = _dbContext.TimeOffRequests
                .Where(t => t.Receiver == user.Email || t.Redirects.Where(r => r.Participant == user.Email).Any())
                .OrderByDescending(t => t.Id)
                .Join(_dbContext.Employees,
                      t => t.Sender,
                      e => e.PersonalInformation.Email,
                      (tof, emp) => new { TimeOffRequest = tof, Sender = emp.PersonalInformation.FullName })
                .Select(x => new ReceivedTimeOffRequestsModel(
                    x.TimeOffRequest.Id.ToString(),
                    x.TimeOffRequest.Status,
                    x.TimeOffRequest.CreateDate,
                    x.Sender,
                    x.TimeOffRequest.Title,
                    new ContentModel(
                        new TimeOffRequestFileModel(x.TimeOffRequest.File.Name, x.TimeOffRequest.File.Path, x.TimeOffRequest.File.Type),
                        string.IsNullOrEmpty(x.TimeOffRequest.Description) ? string.Empty : TimeOffRequestResources.Description,
                        x.TimeOffRequest.Description),
                    (x.TimeOffRequest.Receiver == user.Email) || x.TimeOffRequest.Redirects.Any(r => r.Participant == user.Email && r.RightOfConfirmation))
                )
                .ToList();

            if (!string.IsNullOrEmpty(request.Search))
            {
                timeOffRequests = timeOffRequests
                    .Where(t => t.Number.ToString().ToUpper().Contains(request.Search.ToUpper())
                                || t.Status.ToString().ToUpper().Contains(request.Search.ToUpper())
                                || t.Sender.ToUpper().Contains(request.Search.ToUpper())
                                || (t.Content.Description != null && t.Content.Description.ToUpper().Contains(request.Search.ToUpper()))
                                || t.Title.ToUpper().Contains(request.Search.ToUpper())).ToList();
            }

            return new Result<ReceivedTimeOffRequestsResponse>(new ReceivedTimeOffRequestsResponse(timeOffRequests.Count(), timeOffRequests));
        }

        [HttpGet]
        public Result<TimeOffRequestHistoryResponse> TimeOffRequestHistory([FromQuery] TimeOffRequestHistoryRequest request)
        {
            var timeOffRequest = (
                    from tof in _dbContext.TimeOffRequests
                    join senderEmp in _dbContext.Employees on tof.Sender equals senderEmp.PersonalInformation.Email
                    join receiverEmp in _dbContext.Employees on tof.Receiver equals receiverEmp.PersonalInformation.Email
                    where tof.Id.ToString() == request.Number
                    select new TimeOffRequestHistoryResponse(
                        tof.Type,
                        tof.Id.ToString(),
                        tof.Title,
                        senderEmp.PersonalInformation.FullName,
                        receiverEmp.PersonalInformation.FullName,
                        tof.CreateDate,
                        tof.Deadline,
                        tof.From,
                        tof.Including,
                        new TimeOffRequestFileModel(tof.File.Name, tof.File.Path, tof.File.Type),
                        tof.Description,
                        tof.Status,
                        (from statusChanger in _dbContext.Employees
                         where statusChanger.PersonalInformation.Email == tof.StatusChanger
                         select string.Format(
                             "{0} {1}",
                             statusChanger.PersonalInformation.FullName,
                             tof.Status == TimeOffRequestStatus.Confirmed ? TimeOffRequestResources.ConfirmedRequest : TimeOffRequestResources.RejectedRequest)).FirstOrDefault(),
                        tof.StatusComment,
                        tof.StatusChangeDate,
                        new TimeOffRequestFileModel(tof.StatusFile.Name, tof.StatusFile.Path, tof.StatusFile.Type),
                        tof.Redirects.OrderBy(r => r.Id).Select(r => new RedirectModel(
                            (from redirecter in _dbContext.Employees
                             where redirecter.PersonalInformation.Email == r.Redirecter
                             select redirecter.PersonalInformation.FullName).FirstOrDefault(),
                            (from participant in _dbContext.Employees
                             where participant.PersonalInformation.Email == r.Participant
                             select participant.PersonalInformation.FullName).FirstOrDefault(),
                            r.Comment,
                            new TimeOffRequestFileModel(r.File.Name, r.File.Path, r.File.Type),
                            r.File.Path != null)).ToList()))
                        .FirstOrDefault();

            return new Result<TimeOffRequestHistoryResponse>(timeOffRequest);
        }

        [HttpGet]
        public Result<SentTimeOffRequestDetailsResponse> SentTimeOffRequestDetails([FromQuery] SentTimeOffRequestDetailsRequest request)
        {
            var timeOffRequest = _dbContext.TimeOffRequests
                .Where(t => t.Id.ToString() == request.Number)
                .Select(t => new SentTimeOffRequestDetailsResponse(
                    t.Type,
                    t.Id.ToString(),
                    t.Title,
                    (from receiver in _dbContext.Employees
                     where receiver.PersonalInformation.Email == t.Receiver
                     select receiver.PersonalInformation.FullName).FirstOrDefault(),
                    t.CreateDate,
                    t.Deadline,
                    t.From,
                    t.Including,
                    new TimeOffRequestFileModel(t.File.Name, t.File.Path, t.File.Type),
                    t.Description,
                    t.Status,
                    (from statusChanger in _dbContext.Employees
                     where statusChanger.PersonalInformation.Email == t.StatusChanger
                     select string.Format(
                         "{0} {1}",
                         statusChanger.PersonalInformation.FullName,
                         t.Status == TimeOffRequestStatus.Confirmed ? TimeOffRequestResources.ConfirmedRequest : TimeOffRequestResources.RejectedRequest)).FirstOrDefault(),
                    t.StatusComment,
                    t.StatusChangeDate,
                     new TimeOffRequestFileModel(t.StatusFile.Name, t.StatusFile.Path, t.StatusFile.Type)
                )).FirstOrDefault();

            return new Result<SentTimeOffRequestDetailsResponse>(timeOffRequest);
        }

        [HttpGet]
        public Result<ReceivedTimeOffRequestHistoryResponse> ReceivedTimeOffRequestHistory([FromQuery] ReceivedTimeOffRequestHistoryRequest request)
        {
            if (!User.Identity.IsAuthenticated)
                return new Result<ReceivedTimeOffRequestHistoryResponse>(ErrorResources.Unauthorized);

            var user = _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value).Result;

            var timeOffRequest = _dbContext.TimeOffRequests
                .Where(t => t.Id.ToString() == request.Number)
                .Select(t => new ReceivedTimeOffRequestHistoryResponse(
                    t.Type,
                    t.Id.ToString(),
                    t.Title,
                    (from sender in _dbContext.Employees
                     where sender.PersonalInformation.Email == t.Sender
                     select sender.PersonalInformation.FullName).FirstOrDefault(),
                    t.CreateDate,
                    t.Deadline,
                    t.From,
                    t.Including,
                    new TimeOffRequestFileModel(t.File.Name, t.File.Path, t.File.Type),
                    t.Description,
                    t.Status,
                    (from statusChanger in _dbContext.Employees
                     where statusChanger.PersonalInformation.Email == t.StatusChanger
                     select string.Format(
                         "{0} {1}",
                         statusChanger.PersonalInformation.FullName,
                         t.Status == TimeOffRequestStatus.Confirmed ? TimeOffRequestResources.ConfirmedRequest : TimeOffRequestResources.RejectedRequest)).FirstOrDefault(),
                    t.StatusComment,
                    t.StatusChangeDate,
                    new TimeOffRequestFileModel(t.StatusFile.Name, t.StatusFile.Path, t.StatusFile.Type),
                    t.Redirects.OrderBy(r => r.Id).Select(r => new RedirectModel(
                       (from redirecter in _dbContext.Employees
                        where redirecter.PersonalInformation.Email == r.Redirecter
                        select redirecter.PersonalInformation.FullName).FirstOrDefault(),
                       (from participant in _dbContext.Employees
                        where participant.PersonalInformation.Email == r.Participant
                        select participant.PersonalInformation.FullName).FirstOrDefault(),
                       r.Comment,
                       new TimeOffRequestFileModel(r.File.Name, r.File.Path, r.File.Type),
                       r.File.Path != null)).ToList(),
                       (t.Receiver == user.Email) || t.Redirects.Where(r => r.Participant == user.Email && r.RightOfConfirmation).Any()))
                .FirstOrDefault();

            return new Result<ReceivedTimeOffRequestHistoryResponse>(timeOffRequest);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> RedirectTimeOffRequest([FromForm] RedirecTimeOffRequestRequest request)
        {
            if (!User.Identity.IsAuthenticated)
                return new Result<IEntity[]>(ErrorResources.Unauthorized);

            var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);

            var file = await _fileUploadService.UploadFileAsync(request.File);

            var action = new RedirecTimeOffRequestAction(
                Convert.ToInt32(request.Number),
                user.Email,
                request.Participant,
                file,
                request.Comment,
                request.RightOfConfirmation
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new RedirecTimeOffRequestBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> ChangeReceivedTimeOffRequestStatus([FromForm] ChangeReceivedTimeOffRequestStatusRequest request)
        {
            if (!User.Identity.IsAuthenticated)
                return new Result<IEntity[]>(ErrorResources.Unauthorized);

            var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);

            var file = await _fileUploadService.UploadFileAsync(request.File);

            var action = new ChangeReceivedTimeOffRequestStatusAction(
                Convert.ToInt32(request.Number),
                user.Email,
                request.Status,
                request.Comment,
                file
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new ChangeReceivedTimeOffRequestStatusBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> DeleteTimeOffRequest([FromForm] DeleteTimeOffRequestRequest request)
        {
            var action = new DeleteTimeOffRequestAction(
            //Convert.ToInt32(request.Number),
            //user.Email,
            //request.Status,
            //request.Comment,
            //file
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new DeleteTimeOffRequestBehavior()
                        ).Save(entityStore, action);
        }
    }
}
