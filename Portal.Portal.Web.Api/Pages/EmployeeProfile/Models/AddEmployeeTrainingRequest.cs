namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class AddEmployeeTrainingRequest
    {
        public int EmployeeId { get; set; }

        public string Training { get; set; }

        public IFormFile? File { get; set; }
    }
}
