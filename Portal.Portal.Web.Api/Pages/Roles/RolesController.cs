using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.Portal.Application.AdminPanelModule.CreateGroupFeature;
using Portal.Portal.Application.ApplicationRoles.Models;
using Portal.Portal.Application.RolesModule.AddEmployeeFeature;
using Portal.Portal.Application.RolesModule.CreateRoleFeature;
using Portal.Portal.Application.RolesModule.DeleteEmployeeFeature;
using Portal.Portal.Application.RolesModule.EditRoleFeature;
using Portal.Portal.Application.RolesModule.Models.ChildModels;
using Portal.Portal.Common;
using Portal.Portal.Persistence;
using Portal.Portal.Web.Api.Pages.AdminPanel.Models;
using Portal.Portal.Web.Api.Pages.Roles.Models;
using Portal.Portal.Web.Api.Pages.TimeOffRequests.Models;

namespace Portal.Portal.Web.Api.Pages.Roles
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class RolesController
    {
        private readonly PortalDbContext _dbContext;

        public RolesController(PortalDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public Result<IEntity[]> CreateRole([FromBody] CreateRoleRequest request)
        {
            var allPermissions = _dbContext.PermissionGroups.Include(p => p.Permissions).SelectMany(p => p.Permissions).ToList();

            var action = new CreateRoleAction(
                request.Name,
                request.Description,
                GetPermissions(request)
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new CreateRoleBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public Result<IEntity[]> EditRole([FromBody] EditRoleRequest request)
        {
            var allPermissions = _dbContext.PermissionGroups.Include(p => p.Permissions).SelectMany(p => p.Permissions).ToList();

            var action = new EditRoleAction(
                request.RoleId,
                request.Name,
                request.Description,
                GetPermissions(request)
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new EditRoleBehavior()
                        ).Save(entityStore, action);
        }

        private List<Permission> GetPermissions(BaseRoleRequest request)
        {
            var allPermissions = _dbContext.PermissionGroups.Include(p => p.Permissions).SelectMany(p => p.Permissions).ToList();

            var permissions = new List<Permission>();
            var properties = typeof(BaseRoleRequest).GetProperties();

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(bool) && (bool)property.GetValue(request))
                {
                    var permissionKey = property.Name.Replace("Permission", "");
                    var permission = allPermissions.FirstOrDefault(p => p.PermissionKey == permissionKey);
                    if (permission != null)
                    {
                        permissions.Add(permission);
                    }
                }
            }

            return permissions;
        }

        [HttpGet]
        public Result<PermissionsResponse> Permissions([FromQuery] PermissionsRequest request)
        {
            var basedOnRole = _dbContext.Roles.Include(r => r.Permissions).FirstOrDefault(r => r.Id == request.BasedOnRoleId);

            var employeePermissions = GetPermissionGroupByName("Employee");
            var timeOffRequestsPermissions = GetPermissionGroupByName("Time Off Requests");
            var nonCompliancePermissions = GetPermissionGroupByName("Non Compliance");
            var adminPanelPermissions = GetPermissionGroupByName("Admin Panel");
            var rolesPermissions = GetPermissionGroupByName("Roles");

            var permissionsResponse = new PermissionsResponse(
                new EmployeePermissionsModel(
                    CreatePermissionModel(employeePermissions, "AddEmployee", basedOnRole),
                    CreatePermissionModel(employeePermissions, "ViewEmployeesList", basedOnRole),
                    CreatePermissionModel(employeePermissions, "ViewEmployeeProfile", basedOnRole),
                    CreatePermissionModel(employeePermissions, "EditPersonalInformation", basedOnRole),
                    CreatePermissionModel(employeePermissions, "EditJobInfo", basedOnRole),
                    CreatePermissionModel(employeePermissions, "EditEducation", basedOnRole),
                    CreatePermissionModel(employeePermissions, "EditPayrollBasic", basedOnRole),
                    CreatePermissionModel(employeePermissions, "EditEmploymentHistory", basedOnRole),
                    CreatePermissionModel(employeePermissions, "EditSkillsAndLanguages", basedOnRole),
                    CreatePermissionModel(employeePermissions, "EditOtherInfo", basedOnRole)),
               new TimeOffRequestPermissionsModel(CreatePermissionModel(timeOffRequestsPermissions, "AllTimeOffRequests", basedOnRole)),
               new NonCompliancePermissionsModel(CreatePermissionModel(nonCompliancePermissions, "AllNonComliances", basedOnRole)),
               new AdminPanelPermissionsModel(CreatePermissionModel(adminPanelPermissions, "ViewAdminPanel", basedOnRole)),
               new RolesPermissionsModel(CreatePermissionModel(rolesPermissions, "ViewRoles", basedOnRole))
            );

            return new Result<PermissionsResponse>(permissionsResponse);
        }

        private PermissionGroup GetPermissionGroupByName(string name)
        {
            return _dbContext.PermissionGroups.Include(p => p.Permissions).FirstOrDefault(pg => pg.Name == name);
        }

        private Permission GetPermissionByName(PermissionGroup permissionGroup, string permissionKey)
        {
            return permissionGroup?.Permissions.FirstOrDefault(p => p.PermissionKey == permissionKey);
        }

        private PermissionModel CreatePermissionModel(PermissionGroup permissionGroup, string permissionKey, Role role)
        {
            var permission = GetPermissionByName(permissionGroup, permissionKey);
            var hasPermission = role != null && role.Permissions.Any(p => p.PermissionKey == permission?.PermissionKey);

            return new PermissionModel(
                permission?.Id ?? 0,
                hasPermission,
                permission?.PermissionKey ?? string.Empty,
                permission?.Description ?? string.Empty
            );
        }

        [HttpGet]
        public Result<EmployeesResponse> Employees([FromQuery] string? search)
        {
            var employeeEmails = _dbContext.Employees.Select(e => e.PersonalInformation.Email).ToList();

            if (!string.IsNullOrEmpty(search))
                employeeEmails = employeeEmails.Where(e => e.ToLower().Contains(search.ToLower())).ToList();

            return new Result<EmployeesResponse>(new EmployeesResponse(employeeEmails));
        }

        [HttpPost]
        public Result<IEntity[]> AddEmployee([FromBody] AddEmployeeRequest request)
        {
            var employee = _dbContext.Employees.Include(e => e.PersonalInformation).FirstOrDefault(e => e.PersonalInformation.Email == request.Email);

            var action = new AddEmployeeAction(
                request.RoleId,
                employee.Id,
                employee.PersonalInformation.FullName,
                employee.PersonalInformation.Position
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new AddEmployeeBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public Result<IEntity[]> DeleteEmployee([FromBody] DeleteEmployeeRequest request)
        {
            var action = new DeleteEmployeeAction(
                request.RoleId,
                request.UserId
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new DeleteEmployeeBehavior()
                        ).Save(entityStore, action);
        }

        [HttpGet]
        public Result<RolesListResponse> RolesList([FromQuery] RolesListRequest request)
        {
            var roles = _dbContext.Roles
                .OrderByDescending(r => r.Id)
                .Select(r => new RolesListModel(
                    r.Id,
                    r.Name,
                    r.Description,
                    r.Users.Count))
                .ToList();

            var adminRole = roles.FirstOrDefault(r => r.Name == "Admin");
            var adminRoleIndex = roles.IndexOf(adminRole);
            if (adminRoleIndex > 0)
            {
                roles.RemoveAt(adminRoleIndex);
                roles.Insert(0, adminRole);
            }

            var employeeRole = roles.FirstOrDefault(r => r.Name == "Employee");
            var employeeRoleIndex = roles.IndexOf(employeeRole);
            if (employeeRoleIndex > 0)
            {
                roles.RemoveAt(employeeRoleIndex);
                roles.Insert(1, employeeRole);
            }

            if (!string.IsNullOrEmpty(request.Search))
                roles = roles.Where(r => r.Name.ToLower().Contains(request.Search.ToLower())).ToList();

            return new Result<RolesListResponse>(new RolesListResponse(roles));
        }

        [HttpGet]
        public Result<RoleDetailsResponse> RoleDetails([FromQuery] RoleDetailsRequest request)
        {
            var groups = _dbContext.PermissionGroups.Include(g => g.Permissions).ToList();

            var role = _dbContext.Roles.Include(r => r.Permissions).Include(r => r.Users).FirstOrDefault(r => r.Id == request.RoleId);

            var users = role.Users.Select(u => new RoleUserModel(
                            u.UserId,
                            u.FullName,
                            u.Position))
                        .ToList();

            if (!string.IsNullOrEmpty(request.Search))
                users = users.Where(u => u.FullName.ToLower().Contains(request.Search.ToLower())
                                       || u.Position.ToLower().Contains(request.Search.ToLower())).ToList();

            var response = new RoleDetailsResponse(
                role.Id,
                role.Name,
                role.Description,
                groups.Select(g => new GroupModel(
                    g.Name,
                    g.Permissions.Select(p => new PermissionModel(
                        p.Id,
                        role.Permissions.Select(rp => rp.PermissionId).Contains(p.Id),
                        p.PermissionKey,
                        p.Description))
                    .ToList()))
                .ToList(),
                users
                );

            return new Result<RoleDetailsResponse>(response);
        }
    }
}
