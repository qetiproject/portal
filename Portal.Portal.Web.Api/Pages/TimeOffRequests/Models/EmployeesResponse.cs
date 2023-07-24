namespace Portal.Portal.Web.Api.Pages.TimeOffRequests.Models
{
    public class EmployeesResponse
    {
        public EmployeesResponse(List<string> emails)
        {
            Emails = emails;
        }

        public List<string> Emails { get; set; }
    }
}
