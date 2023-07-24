namespace Portal.Portal.Web.Api.Pages.AdminPanel.Models
{
    public class DownloadEmployeeImportFileResponse
    {
        public DownloadEmployeeImportFileResponse(string path, string name)
        {
            Path = path;
            Name = name;
        }

        public string Path { get; set; }

        public string Name { get; set; }
    }
}
