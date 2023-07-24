namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class AddFormerPositionRequest
    {
        public int EmployeeId { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
