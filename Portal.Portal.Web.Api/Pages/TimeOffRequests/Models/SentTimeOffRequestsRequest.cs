using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;
using Portal.Portal.Common;

namespace Portal.Portal.Web.Api.Pages.TimeOffRequests.Models
{
    public class SentTimeOffRequestsRequest
    {
        public string? Search { get; set; }
    }

    public class SentTimeOffRequestsResponse
    {
        public SentTimeOffRequestsResponse(int total, List<SentTimeOffRequestsModel> timeOffRequests)
        {
            Total = total;
            TimeOffRequests = timeOffRequests;
        }

        public int Total { get; set; }

        public List<SentTimeOffRequestsModel> TimeOffRequests { get; set; }
    }

    public class SentTimeOffRequestsModel
    {
        public SentTimeOffRequestsModel(
            string number,
            TimeOffRequestStatus status,
            DateTime date,
            string receiver,
            string title,
            ContentModel content)
        {
            Number = number;
            Status = status;
            Date = date;
            Receiver = receiver;
            Title = title;
            Content = content;
        }

        public string Number { get; set; }

        public TimeOffRequestStatus Status { get; set; }

        public DateTime Date { get; set; }

        public string Receiver { get; set; }

        public string Title { get; set; }

        public ContentModel Content { get; set; }
    }
}
