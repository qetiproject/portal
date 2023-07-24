namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class AddEmployeeUniversityRequest
    {
        public int EmployeeId { get; set; }

        public string University { get; set; }

        public string Faculty { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IFormFile? File { get; set; }
    }
}
