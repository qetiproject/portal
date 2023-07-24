namespace Portal.Portal.Web.Api.Pages.Roles.Models
{
    public class PermissionsRequest
    {
        public int? BasedOnRoleId { get; set; }
    }

    public class PermissionsResponse
    {
        public PermissionsResponse(
            EmployeePermissionsModel employeeModule,
            TimeOffRequestPermissionsModel timeOffRequestModule,
            NonCompliancePermissionsModel nonComlianceModule,
            AdminPanelPermissionsModel adminPanelModule,
            RolesPermissionsModel rolesModel)
        {
            EmployeeModule = employeeModule;
            TimeOffRequestModule = timeOffRequestModule;
            NonComlianceModule = nonComlianceModule;
            AdminPanelModule = adminPanelModule;
            RolesModel = rolesModel;
        }

        public EmployeePermissionsModel EmployeeModule { get; set; }

        public TimeOffRequestPermissionsModel TimeOffRequestModule { get; set; }

        public NonCompliancePermissionsModel NonComlianceModule { get; set; }

        public AdminPanelPermissionsModel AdminPanelModule { get; set; }

        public RolesPermissionsModel RolesModel { get; set; }
    }

    public class EmployeePermissionsModel
    {
        public EmployeePermissionsModel(
            PermissionModel addEmployeePermission,
            PermissionModel viewEmployeesListPermission,
            PermissionModel viewEmployeeProfilePermission,
            PermissionModel editPersonalInformationPermission,
            PermissionModel editJobInfoPermission,
            PermissionModel editEducationPermission,
            PermissionModel editPayrollBasicPermission,
            PermissionModel editEmploymentHistoryPermission,
            PermissionModel editSkillsAndLanguagesPermission,
            PermissionModel editOtherInfoPermission)
        {
            AddEmployeePermission = addEmployeePermission;
            ViewEmployeesListPermission = viewEmployeesListPermission;
            EditPersonalInformationPermission = editPersonalInformationPermission;
            EditJobInfoPermission = editJobInfoPermission;
            EditEducationPermission = editEducationPermission;
            EditPayrollBasicPermission = editPayrollBasicPermission;
            EditEmploymentHistoryPermission = editEmploymentHistoryPermission;
            EditSkillsAndLanguagesPermission = editSkillsAndLanguagesPermission;
            EditOtherInfoPermission = editOtherInfoPermission;
            ViewEmployeeProfilePermission = viewEmployeeProfilePermission;
        }

        public PermissionModel AddEmployeePermission { get; set; }
        public PermissionModel ViewEmployeesListPermission { get; set; }
        public PermissionModel ViewEmployeeProfilePermission { get; set; }
        public PermissionModel EditPersonalInformationPermission { get; set; }
        public PermissionModel EditJobInfoPermission { get; set; }
        public PermissionModel EditEducationPermission { get; set; }
        public PermissionModel EditPayrollBasicPermission { get; set; }
        public PermissionModel EditEmploymentHistoryPermission { get; set; }
        public PermissionModel EditSkillsAndLanguagesPermission { get; set; }
        public PermissionModel EditOtherInfoPermission { get; set; }
    }

    public class TimeOffRequestPermissionsModel
    {
        public TimeOffRequestPermissionsModel(PermissionModel allTimeOffRequestsPermission)
        {
            AllTimeOffRequestsPermission = allTimeOffRequestsPermission;
        }

        public PermissionModel AllTimeOffRequestsPermission { get; set; }
    }

    public class NonCompliancePermissionsModel
    {
        public NonCompliancePermissionsModel(PermissionModel allNonComliancesPermission)
        {
            AllNonComliancesPermission = allNonComliancesPermission;
        }

        public PermissionModel AllNonComliancesPermission { get; set; }
    }

    public class AdminPanelPermissionsModel
    {
        public AdminPanelPermissionsModel(PermissionModel viewAdminPanelPermission)
        {
            ViewAdminPanelPermission = viewAdminPanelPermission;
        }

        public PermissionModel ViewAdminPanelPermission { get; set; }
    }

    public class RolesPermissionsModel
    {
        public RolesPermissionsModel(PermissionModel viewRolesPermission)
        {
            ViewRolesPermission = viewRolesPermission;
        }

        public PermissionModel ViewRolesPermission { get; set; }
    }
}
