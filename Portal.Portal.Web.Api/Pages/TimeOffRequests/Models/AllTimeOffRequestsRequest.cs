using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;
using Portal.Portal.Common;

namespace Portal.Portal.Web.Api.Pages.TimeOffRequests.Models
{
    public class AllTimeOffRequestsRequest
    {
        public string? Search { get; set; }
    }

    public class AllTimeOffRequestsResponse
    {
        public AllTimeOffRequestsResponse(int total, List<AllTimeOffRequestsModel> timeOffRequests)
        {
            Total = total;
            TimeOffRequests = timeOffRequests;
        }

        public int Total { get; set; }

        public List<AllTimeOffRequestsModel> TimeOffRequests { get; set; }
    }

    public class AllTimeOffRequestsModel
    {
        public AllTimeOffRequestsModel(
            string number,
            TimeOffRequestStatus status,
            DateTime date,
            string sender,
            string receiver,
            string title,
            ContentModel content)
        {
            Number = number;
            Status = status;
            Date = date;
            Sender = sender;
            Receiver = receiver;
            Title = title;
            Content = content;
        }

        public string Number { get; set; }

        public TimeOffRequestStatus Status { get; set; }

        public DateTime Date { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public string Title { get; set; }

        public ContentModel Content { get; set; }
    }
}
