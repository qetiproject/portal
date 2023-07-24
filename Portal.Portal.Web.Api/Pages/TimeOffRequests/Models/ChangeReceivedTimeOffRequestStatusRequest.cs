using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;

namespace Portal.Portal.Web.Api.Pages.TimeOffRequests.Models
{
    public class ChangeReceivedTimeOffRequestStatusRequest
    {
        public string Number { get; set; }

        public TimeOffRequestStatus Status { get; set; }

        public string? Comment { get; set; }

        public IFormFile? File { get; set; }
    }
}
