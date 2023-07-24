namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class EditEmployeeTrainingRequest
    {
        public int EmployeeId { get; set; }

        public int TrainingId { get; set; }

        public string Training { get; set; }

        public IFormFile? File { get; set; }
    }
}
