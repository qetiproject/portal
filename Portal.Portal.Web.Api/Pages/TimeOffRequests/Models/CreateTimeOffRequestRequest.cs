using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;

namespace Portal.Portal.Web.Api.Pages.TimeOffRequests.Models
{
    public class CreateTimeOffRequestRequest
    {
        public TimeOffRequestType Type { get; set; }

        public string Title { get; set; }

        public Recipient Recipient { get; set; }

        public string Receiver { get; set; }

        public string? DeadLine { get; set; }

        public string? From { get; set; }

        public string? Including { get; set; }

        public IFormFile? File { get; set; }

        public string? Description { get; set; }
    }
}
