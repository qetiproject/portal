namespace Portal.Portal.Web.Api.Pages.Roles.Models
{
    public class PermissionModel
    {
        public PermissionModel(
            int permissionId,
            bool active,
            string key,
            string description)
        {
            PermissionId = permissionId;
            Active = active;
            this.Description = description;
            Key = key;
        }

        public int PermissionId { get; set; }

        public bool Active { get; set; }

        public string Key { get; set; }

        public string Description { get; set; }
    }
}
