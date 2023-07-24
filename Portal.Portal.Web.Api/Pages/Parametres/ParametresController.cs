using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Portal.Application.AdminPanelModule.CreateContractTypeFeature;
using Portal.Portal.Application.AdminPanelModule.CreateGroupFeature;
using Portal.Portal.Application.AdminPanelModule.UpdateTimeOffRequestPanelFeature;
using Portal.Portal.Application.ApplicationUsers.Models;
using Portal.Portal.Common;
using Portal.Portal.Persistence;
using Portal.Portal.Web.Api.Pages.AdminPanel.Models;
using Portal.Portal.Web.Api.Pages.Shared.FileDownload.Models;
using Portal.Portal.Web.Api.Pages.TimeOffRequests.Models;
using Portal.Portal.Web.Api.Resources;
using System.Security.Claims;

namespace Portal.Portal.Web.Api.Pages.AdminPanel
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class ParametresController : ControllerBase
    {
        private readonly PortalDbContext _dbContext;

        public ParametresController(PortalDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public Result<IEntity[]> CreateGroup([FromQuery] CreateGroupRequest request)
        {
            var action = new CreateGroupAction(
                request.Group
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new CreateGroupBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public Result<IEntity[]> DeleteGroup([FromQuery] DeleteGroupRequest request)
        {
            var group = _dbContext.Groups.FirstOrDefault(g => g.Id == request.Id);
            if (group == null)
            {
                return new Result<IEntity[]>("NotFound");
            }

            _dbContext.Groups.Remove(group);

            _dbContext.SaveChanges();

            return new Result<IEntity[]>();
        }

        [HttpGet]
        public Result<GroupsListResponse> GroupsList([FromQuery] GroupsListRequest request)
        {
            var groups = _dbContext.Groups.Select(g => new GroupsListModel(g.Id, g.Name)).ToList();

            if (!string.IsNullOrEmpty(request.Search))
                groups = groups.Where(g => g.Group.ToLower().Contains(request.Search.ToLower())).ToList();

            return new Result<GroupsListResponse>(new GroupsListResponse(groups));
        }

        [HttpPost]
        public Result<IEntity[]> CreateContractType([FromQuery] CreateContractTypeRequest request)
        {
            var action = new CreateContractTypeAction(
                request.ContractType
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new CreateContractTypeBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public Result<IEntity[]> DeleteContractType([FromQuery] DeleteContractTypeRequest request)
        {
            var contractType = _dbContext.ContractTypes.FirstOrDefault(g => g.Id == request.Id);
            if (contractType == null)
            {
                return new Result<IEntity[]>("NotFound");
            }

            _dbContext.ContractTypes.Remove(contractType);

            _dbContext.SaveChanges();

            return new Result<IEntity[]>();
        }

        [HttpGet]
        public Result<ContractTypesListResponse> ContractTypesList([FromQuery] ContractTypesListRequest request)
        {
            var ContractTypes = _dbContext.ContractTypes.Select(g => new ContractTypesListModel(g.Id, g.Name)).ToList();

            if (!string.IsNullOrEmpty(request.Search))
                ContractTypes = ContractTypes.Where(g => g.ContractType.ToLower().Contains(request.Search.ToLower())).ToList();

            return new Result<ContractTypesListResponse>(new ContractTypesListResponse(ContractTypes));
        }


        [HttpGet]
        public async Task<IActionResult> DownloadEmployeeImportFile()
        {
            var memory = new MemoryStream();
            using (var stream = new FileStream("App_Data\\Drafts\\Employees Import.xlsx", FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, "application/octet-stream", "Employees Import.xlsx");
        }

        [HttpGet]
        public Result<EmployeesResponse> Employees([FromQuery] string? search)
        {
            var employeeEmails = _dbContext.Employees.Select(e => e.PersonalInformation.Email).ToList();

            if (!string.IsNullOrEmpty(search))
                employeeEmails = employeeEmails.Where(e => e.ToLower().Contains(search.ToLower())).ToList();

            return new Result<EmployeesResponse>(new EmployeesResponse(employeeEmails));
        }

        [HttpGet]
        public Result<GetTimeOffRequestPanelResponse> GetTimeOffRequestPanel()
        {
            var timeOffRequestPanel = _dbContext.TimeOffRequestPanel.FirstOrDefault();

            return new Result<GetTimeOffRequestPanelResponse>(new GetTimeOffRequestPanelResponse(timeOffRequestPanel?.HRWhoReceivesTimeOffRequests));
        }

        [HttpPost]
        public Result<IEntity[]> UpdateTimeOffRequestPanel([FromBody] UpdateTimeOffRequestPanelRequests request)
        {
            var timeOffRequestPanel = _dbContext.TimeOffRequestPanel.FirstOrDefault();

            var action = new UpdateTimeOffRequestPanelAction(
                timeOffRequestPanel?.Id,
                request.HRWhoReceivesTimeOffRequests
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new UpdateTimeOffRequestPanelBehavior()
                        ).Save(entityStore, action);
        }
    }
}
