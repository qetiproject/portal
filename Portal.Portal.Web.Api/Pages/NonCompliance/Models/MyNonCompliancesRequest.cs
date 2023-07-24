using Portal.Portal.Application.NonComplianceModule.Models.Enums;

namespace Portal.Portal.Web.Api.Pages.NonCompliance.Models
{
    public class MyNonCompliancesRequest
    {
        public string? Search { get; set; }
    }

    public class MyNonCompliancesResponse
    {
        public MyNonCompliancesResponse(int total, List<MyNonCompliancesModel> nonCompliances)
        {
            Total = total;
            NonCompliances = nonCompliances;
        }

        public int Total { get; set; }

        public List<MyNonCompliancesModel> NonCompliances { get; set; }
    }

    public class MyNonCompliancesModel
    {
        public MyNonCompliancesModel(
            string number,
            string group,
            NonComplianceStatus status,
            DateTime date,
            string sender,
            NonComplianceContentModel content)
        {
            Number = number;
            Group = group;
            Status = status;
            Date = date;
            Sender = sender;
            Content = content;
        }

        public string Number { get; set; }

        public string Group { get; set; }

        public NonComplianceStatus Status { get; set; }

        public DateTime Date { get; set; }

        public string Sender { get; set; }

        public NonComplianceContentModel Content { get; set; }
    }
}
