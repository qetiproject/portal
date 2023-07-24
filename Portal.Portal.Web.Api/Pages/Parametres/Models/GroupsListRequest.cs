namespace Portal.Portal.Web.Api.Pages.AdminPanel.Models
{
    public class GroupsListRequest
    {
        public string? Search { get; set; }
    }

    public class GroupsListResponse
    {
        public GroupsListResponse(List<GroupsListModel> groups)
        {
            Groups = groups;
        }

        public List<GroupsListModel> Groups { get; set; }
    }

    public class GroupsListModel
    {
        public GroupsListModel(int id, string group)
        {
            Id = id;
            Group = group;
        }

        public int Id { get; set; }

        public string Group { get; set; }
    }
}
