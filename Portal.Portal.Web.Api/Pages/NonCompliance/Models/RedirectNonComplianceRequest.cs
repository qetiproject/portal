using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.NonCompliance.Models
{
    public class RedirectNonComplianceRequest
    {
        [Required]
        public string Number { get; set; }

        [Required]
        public string Receiver { get; set; }

        public IFormFile? File { get; set; }

        public string? Comment { get; set; }

        [Required]
        public bool StatusChange { get; set; }
    }
}
