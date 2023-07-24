namespace Portal.Portal.Web.Api.Pages.AdminPanel.Models
{
    public class GetTimeOffRequestPanelResponse
    {
        public GetTimeOffRequestPanelResponse(string? hRWhoReceivesTimeOffRequests)
        {
            HRWhoReceivesTimeOffRequests = hRWhoReceivesTimeOffRequests;
        }

        public string? HRWhoReceivesTimeOffRequests { get; set; }
    }
}
