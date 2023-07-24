namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class EditFormerPositionRequest
    {
        public int EmployeeId { get; set; }

        public int FormerPositionId { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
