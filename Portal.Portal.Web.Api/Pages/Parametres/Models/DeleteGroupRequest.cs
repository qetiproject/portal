using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.AdminPanel.Models
{
    public class DeleteGroupRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
