using Portal.Portal.Common;

namespace Portal.Portal.Web.Api.Pages.NonCompliance.Models
{
    public class NonComplianceFileModel
    {
        public NonComplianceFileModel(
            string? name,
            string? path,
            FileType? type)
        {
            Name = name;
            Path = path;
            Type = type;
        }

        public string? Name { get; set; }

        public string? Path { get; set; }

        public FileType? Type { get; set; }
    }
}
