using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;
using Portal.Portal.Common;

namespace Portal.Portal.Web.Api.Pages.TimeOffRequests.Models
{
    public class ReceivedTimeOffRequestsRequest
    {
        public string? Search { get; set; }
    }

    public class ReceivedTimeOffRequestsResponse
    {
        public ReceivedTimeOffRequestsResponse(int total, List<ReceivedTimeOffRequestsModel> timeOffRequests)
        {
            Total = total;
            TimeOffRequests = timeOffRequests;
        }

        public int Total { get; set; }

        public List<ReceivedTimeOffRequestsModel> TimeOffRequests { get; set; }
    }

    public class ReceivedTimeOffRequestsModel
    {
        public ReceivedTimeOffRequestsModel(
            string number,
            TimeOffRequestStatus status,
            DateTime date,
            string sender,
            string title,
            ContentModel content,
            bool isStatusChanger)
        {
            Number = number;
            Status = status;
            Date = date;
            Sender = sender;
            Title = title;
            Content = content;
            IsStatusChanger = isStatusChanger;
        }

        public string Number { get; set; }

        public TimeOffRequestStatus Status { get; set; }

        public DateTime Date { get; set; }

        public string Sender { get; set; }

        public string Title { get; set; }

        public ContentModel Content { get; set; }

        public bool IsStatusChanger { get; set; }
    }
}
