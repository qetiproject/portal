namespace Portal.Portal.Web.Api.Pages.Login.Models
{
    public class LoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public LoginResponse(
            int userId,
            string fullName,
            string email,
            string token,
            DateTime expiration,
            string photoPath,
            int employeeId,
            ActionPermissions actionPermissions)
        {
            UserId = userId;
            Email = email;
            Token = token;
            Expiration = expiration;
            FullName = fullName;
            PhotoPath = photoPath;
            EmployeeId = employeeId;
            ActionPermissions = actionPermissions;
        }

        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public string PhotoPath { get; set; }

        public int EmployeeId { get; set; }

        public ActionPermissions ActionPermissions { get; set; }
    }

    public class ActionPermissions
    {
        public ActionPermissions(
            bool addEmployee,
            bool viewEmployeesList,
            bool viewEmployeeProfile,
            bool editPersonalInformation,
            bool editJobInfo,
            bool editEducation,
            bool editPayrollBasic,
            bool editEmploymentHistory,
            bool editSkillsAndLanguages,
            bool editOtherInfo,
            bool allTimeOffRequests,
            bool allNonCompliances,
            bool viewAdminPanel,
            bool viewRoles)
        {
            AddEmployee = addEmployee;
            ViewEmployeesList = viewEmployeesList;
            EditPersonalInformation = editPersonalInformation;
            EditJobInfo = editJobInfo;
            EditEducation = editEducation;
            EditPayrollBasic = editPayrollBasic;
            EditEmploymentHistory = editEmploymentHistory;
            EditSkillsAndLanguages = editSkillsAndLanguages;
            EditOtherInfo = editOtherInfo;
            AllTimeOffRequests = allTimeOffRequests;
            AllNonCompliances = allNonCompliances;
            ViewAdminPanel = viewAdminPanel;
            ViewRoles = viewRoles;
            ViewEmployeeProfile = viewEmployeeProfile;
        }

        public bool AddEmployee { get; set; }

        public bool ViewEmployeesList { get; set; }

        public bool ViewEmployeeProfile { get; set; }

        public bool EditPersonalInformation { get; set; }

        public bool EditJobInfo { get; set; }

        public bool EditEducation { get; set; }

        public bool EditPayrollBasic { get; set; }

        public bool EditEmploymentHistory { get; set; }

        public bool EditSkillsAndLanguages { get; set; }

        public bool EditOtherInfo { get; set; }

        public bool AllTimeOffRequests { get; set; }

        public bool AllNonCompliances { get; set; }

        public bool ViewAdminPanel { get; set; }

        public bool ViewRoles { get; set; }
    }
}
