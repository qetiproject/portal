namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class UpdateEmployeeProfilePhotoRequest
    {
        public int Id { get; set; }

        public IFormFile Photo { get; set; }
    }
}
