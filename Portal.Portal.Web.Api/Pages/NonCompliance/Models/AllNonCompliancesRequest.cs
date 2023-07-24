using Portal.Portal.Application.NonComplianceModule.Models.Enums;
using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;
using Portal.Portal.Web.Api.Pages.TimeOffRequests.Models;

namespace Portal.Portal.Web.Api.Pages.NonCompliance.Models
{
    public class AllNonCompliancesRequest
    {
        public string? Search { get; set; }
    }

    public class AllNonCompliancesResponse
    {
        public AllNonCompliancesResponse(int total, List<AllNonCompliancesModel> nonCompliances)
        {
            Total = total;
            NonCompliances = nonCompliances;
        }

        public int Total { get; set; }

        public List<AllNonCompliancesModel> NonCompliances { get; set; }
    }

    public class AllNonCompliancesModel
    {
        public AllNonCompliancesModel(
            string number,
            string group,
            NonComplianceStatus status,
            DateTime date,
            string sender,
            string violator,
            NonComplianceContentModel content)
        {
            Number = number;
            Group = group;
            Status = status;
            Date = date;
            Sender = sender;
            Violator = violator;
            Content = content;
        }

        public string Number { get; set; }

        public string Group { get; set; }

        public NonComplianceStatus Status { get; set; }

        public DateTime Date { get; set; }

        public string Sender { get; set; }

        public string Violator { get; set; }

        public NonComplianceContentModel Content { get; set; }
    }
}
