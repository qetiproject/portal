using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.AdminPanel.Models
{
    public class CreateContractTypeRequest
    {
        [Required]
        public string ContractType { get; set; }
    }
}
