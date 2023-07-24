namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class EditComputerSkillRequest
    {
        public int EmployeeId { get; set; }

        public int SkillId { get; set; }

        public string Skill { get; set; }
    }
}
