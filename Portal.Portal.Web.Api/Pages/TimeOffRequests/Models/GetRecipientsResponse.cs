namespace Portal.Portal.Web.Api.Pages.TimeOffRequests.Models
{
    public class GetRecipientsResponse
    {
        public GetRecipientsResponse(bool hasHR, bool hasSupervisor)
        {
            HasHR = hasHR;
            HasSupervisor = hasSupervisor;
        }

        public bool HasHR { get; set; }

        public bool HasSupervisor { get; set; }
    }
}
