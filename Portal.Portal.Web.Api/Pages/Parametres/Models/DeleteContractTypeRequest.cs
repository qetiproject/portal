using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.AdminPanel.Models
{
    public class DeleteContractTypeRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
