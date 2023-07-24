using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class SupervisorEmployeesRequest
    {
        [Required]
        public int EmployeeId { get; set; }

        public string? Search { get; set; }
    }
}
