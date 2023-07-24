namespace Portal.Portal.Web.Api.Pages.Employee.Models
{
    public class EmployeeRolesListRequest
    {
        public string? Search { get; set; }
    }

    public class EmployeeRolesListResponse
    {
        public EmployeeRolesListResponse(
            List<EmployeeRolesListModel> roles,
            EmployeeRolesListModel defaultRole)
        {
            Roles = roles;
            DefaultRole = defaultRole;
        }

        public List<EmployeeRolesListModel> Roles { get; set; } = new List<EmployeeRolesListModel>();

        public EmployeeRolesListModel DefaultRole { get; set; }
    }

    public class EmployeeRolesListModel
    {
        public EmployeeRolesListModel(
            int id,
            string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
