using Portal.Portal.Application.NonComplianceModule.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.NonCompliance.Models
{
    public class ChangeNonComplianceStatusRequest
    {
        [Required]
        public string Number { get; set; }

        [Required]
        public NonComplianceStatus Status { get; set; }

        public string? Comment { get; set; }

        public IFormFile? File { get; set; }
    }
}
