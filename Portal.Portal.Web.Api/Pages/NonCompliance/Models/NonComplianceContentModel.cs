using Portal.Portal.Web.Api.Pages.TimeOffRequests.Models;

namespace Portal.Portal.Web.Api.Pages.NonCompliance.Models
{
    public class NonComplianceContentModel
    {
        public NonComplianceContentModel(
            NonComplianceFileModel file,
            string description,
            string? descriptionHover)
        {
            File = file;
            Description = description;
            DescriptionHover = descriptionHover;
        }

        public NonComplianceFileModel? File { get; set; }

        public string? Description { get; set; }

        public string? DescriptionHover { get; set; }
    }
}
