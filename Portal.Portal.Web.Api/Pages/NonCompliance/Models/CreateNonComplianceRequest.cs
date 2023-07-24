using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.NonCompliance.Models
{
    public class CreateNonComplianceRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Violator { get; set; }

        public string? ApprovalDeadline { get; set; }

        public decimal? Fine { get; set; }

        public IFormFile? File { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
