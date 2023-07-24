using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.NonCompliance.Models
{
    public class AddNonComplianceCommentRequest
    {
        [Required]
        public string Number { get; set; }

        [Required]
        public string Comment { get; set; }

        public IFormFile? File { get; set; }
    }
}
