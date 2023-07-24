using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class HideEmployeeProfilePhotoRequest
    {
        [Required]
        public int Id { get; set; }

        public bool Hidden { get; set; }
    }
}
