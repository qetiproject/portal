namespace Portal.Portal.Web.Api.Pages.TimeOffRequests.Models
{
    public class ContentModel
    {
        public ContentModel(
            TimeOffRequestFileModel file,
            string description,
            string? descriptionHover)
        {
            File = file;
            Description = description;
            DescriptionHover = descriptionHover;
        }

        public TimeOffRequestFileModel? File { get; set; }

        public string? Description { get; set; }

        public string? DescriptionHover { get; set; }
    }
}
