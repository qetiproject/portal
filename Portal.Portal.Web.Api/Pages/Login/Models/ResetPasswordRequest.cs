namespace Portal.Portal.Web.Api.Pages.Login.Models
{
    public class ResetPasswordRequest
    {
        public ResetPasswordRequest()
        {
            
        }

        public ResetPasswordRequest(string email, string token)
        {
            Email = email;
            Token = token;
        }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}
