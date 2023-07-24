using Portal.Portal.Common;

namespace Portal.Portal.Web.Api.Pages.TimeOffRequests.Models
{
    public class TimeOffRequestFileModel
    {
        public TimeOffRequestFileModel(
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
