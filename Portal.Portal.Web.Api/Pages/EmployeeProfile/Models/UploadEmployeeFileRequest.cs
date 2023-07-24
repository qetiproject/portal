namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class UploadEmployeeFileRequest
    {
        public int EmployeeId { get; set; }

        public IFormFile? File { get; set; }
    }
}
