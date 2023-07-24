namespace Portal.Portal.Web.Api.Pages.Roles.Models
{
    public class RolesListRequest
    {
        public string? Search { get; set; }
    }

    public class RolesListResponse
    {
        public RolesListResponse(List<RolesListModel> roles)
        {
            Roles = roles;
        }

        public List<RolesListModel> Roles { get; set; } = new List<RolesListModel>();
    }

    public class RolesListModel
    {
        public RolesListModel(
            int id,
            string name,
            string description,
            int numberOfEmployees)
        {
            Id = id;
            Name = name;
            Description = description;
            NumberOfEmployees = numberOfEmployees;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int NumberOfEmployees { get; set; }
    }
}
