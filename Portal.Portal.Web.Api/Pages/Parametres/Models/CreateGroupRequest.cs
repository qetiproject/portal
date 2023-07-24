using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.AdminPanel.Models
{
    public class CreateGroupRequest
    {
        [Required]
        public string Group { get; set; }
    }
}
