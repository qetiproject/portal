using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.Login.Models
{
    public class ForgotPasswordRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
