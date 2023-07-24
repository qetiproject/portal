using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.Roles.Models
{
    public class RoleDetailsRequest
    {
        [Required]
        public int RoleId { get; set; }

        public string? Search { get; set; }
    }

    public class RoleDetailsResponse
    {
        public RoleDetailsResponse(
            int id,
            string name,
            string description,
            List<GroupModel> groups,
            List<RoleUserModel> users)
        {
            Id = id;
            Name = name;
            Description = description;
            Groups = groups;
            Users = users;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<GroupModel> Groups { get; set; }

        public List<RoleUserModel> Users { get; set; }

    }

    public class GroupModel
    {
        public GroupModel(
            string name,
            List<PermissionModel> permissions)
        {
            Name = name;
            Permissions = permissions;
        }

        public string Name { get; set; }

        public List<PermissionModel> Permissions { get; set; }
    }

    public class RoleUserModel
    {
        public RoleUserModel(
            int userId,
            string fullName,
            string position)
        {
            UserId = userId;
            FullName = fullName;
            Position = position;
        }

        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Position { get; set; }
    }
}
