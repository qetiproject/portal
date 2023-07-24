using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.Roles.Models
{
    public class DeleteEmployeeRequest
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
