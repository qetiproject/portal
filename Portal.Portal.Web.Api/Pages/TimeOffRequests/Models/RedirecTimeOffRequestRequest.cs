using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;

namespace Portal.Portal.Web.Api.Pages.TimeOffRequests.Models
{
    public class RedirecTimeOffRequestRequest
    {
        public string Number { get; set; }

        public string Participant { get; set; }

        public IFormFile? File { get; set; }

        public string? Comment { get; set; }

        public bool RightOfConfirmation { get; set; }
    }
}
