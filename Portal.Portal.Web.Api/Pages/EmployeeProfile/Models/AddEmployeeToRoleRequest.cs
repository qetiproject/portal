using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class AddEmployeeToRoleRequest
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
