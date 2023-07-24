using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.Roles.Models
{
    public class AddEmployeeRequest
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
