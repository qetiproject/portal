namespace Portal.Portal.Web.Api.Pages.Roles.Models
{
    public class BaseRoleRequest
    {
        public bool AddEmployeePermission { get; set; }

        public bool ViewEmployeesListPermission { get; set; }

        public bool EditPersonalInformationPermission { get; set; }

        public bool EditJobInfoPermission { get; set; }

        public bool EditEducationPermission { get; set; }

        public bool EditPayrollBasicPermission { get; set; }

        public bool EditEmploymentHistoryPermission { get; set; }

        public bool EditSkillsAndLanguagesPermission { get; set; }

        public bool EditOtherInfoPermission { get; set; }

        public bool AllTimeOffRequestsPermission { get; set; }

        public bool AllNonComliancesPermission { get; set; }

        public bool ViewAdminPanelPermission { get; set; }

        public bool ViewRolesPermission { get; set; }
    }
}
