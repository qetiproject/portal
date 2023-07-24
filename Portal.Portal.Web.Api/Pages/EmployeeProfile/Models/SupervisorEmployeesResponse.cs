namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class SupervisorEmployeesResponse
    {
        public SupervisorEmployeesResponse(List<SupervisorEmployeesModel> employees)
        {
            Employees = employees;
        }

        public List<SupervisorEmployeesModel> Employees { get; set; }
    }

    public class SupervisorEmployeesModel
    {
        public SupervisorEmployeesModel(int id, string email)
        {
            Id = id;
            Email = email;
        }

        public int Id { get; set; }

        public string Email { get; set; }
    }
}
