using Portal.Portal.Application.NonComplianceModule.Models.Enums;

namespace Portal.Portal.Web.Api.Pages.NonCompliance.Models
{
    public class SentNonCompliancesRequest
    {
        public string? Search { get; set; }
    }

    public class SentNonCompliancesResponse
    {
        public SentNonCompliancesResponse(int total, List<SentNonCompliancesModel> nonCompliances)
        {
            Total = total;
            NonCompliances = nonCompliances;
        }

        public int Total { get; set; }

        public List<SentNonCompliancesModel> NonCompliances { get; set; }
    }

    public class SentNonCompliancesModel
    {
        public SentNonCompliancesModel(
            string number,
            string group,
            NonComplianceStatus status,
            DateTime date,
            string violator,
            NonComplianceContentModel content)
        {
            Number = number;
            Group = group;
            Status = status;
            Date = date;
            Violator = violator;
            Content = content;
        }

        public string Number { get; set; }

        public string Group { get; set; }

        public NonComplianceStatus Status { get; set; }

        public DateTime Date { get; set; }

        public string Violator { get; set; }

        public NonComplianceContentModel Content { get; set; }
    }
}
