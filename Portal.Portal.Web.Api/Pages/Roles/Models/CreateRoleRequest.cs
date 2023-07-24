using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.Roles.Models
{
    public class CreateRoleRequest : BaseRoleRequest
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
