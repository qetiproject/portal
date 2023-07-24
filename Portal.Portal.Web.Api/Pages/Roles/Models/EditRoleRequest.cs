using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.Roles.Models
{
    public class EditRoleRequest : BaseRoleRequest
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
