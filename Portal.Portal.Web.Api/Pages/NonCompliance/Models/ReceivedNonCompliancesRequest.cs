using Portal.Portal.Application.NonComplianceModule.Models.Enums;

namespace Portal.Portal.Web.Api.Pages.NonCompliance.Models
{
    public class ReceivedNonCompliancesRequest
    {
        public string? Search { get; set; }
    }

    public class ReceivedNonCompliancesResponse
    {
        public ReceivedNonCompliancesResponse(int total, List<ReceivedNonCompliancesModel> nonCompliances)
        {
            Total = total;
            NonCompliances = nonCompliances;
        }

        public int Total { get; set; }

        public List<ReceivedNonCompliancesModel> NonCompliances { get; set; }
    }

    public class ReceivedNonCompliancesModel
    {
        public ReceivedNonCompliancesModel(
            string number,
            string group,
            NonComplianceStatus status,
            DateTime date,
            string sender,
            string violator,
            NonComplianceContentModel content,
            bool isStatusChanger)
        {
            Number = number;
            Group = group;
            Status = status;
            Date = date;
            Sender = sender;
            Violator = violator;
            Content = content;
            IsStatusChanger = isStatusChanger;
        }

        public string Number { get; set; }

        public string Group { get; set; }

        public NonComplianceStatus Status { get; set; }

        public DateTime Date { get; set; }

        public string Sender { get; set; }

        public string Violator { get; set; }

        public NonComplianceContentModel Content { get; set; }

        public bool IsStatusChanger { get; set; }
    }
}
