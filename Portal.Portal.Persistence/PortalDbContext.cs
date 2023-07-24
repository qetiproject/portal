using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.Portal.Application.AdminPanelModule.Models;
using Portal.Portal.Application.ApplicationRoles.Models;
using Portal.Portal.Application.ApplicationUsers.Models;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.NonComplianceModule.Models;
using Portal.Portal.Application.RolesModule.Models.ChildModels;
using Portal.Portal.Application.TimeOffRequestsModule.Models;

namespace Portal.Portal.Persistence
{
    public class PortalDbContext : IdentityDbContext<User, Role, int>
    {
        public PortalDbContext(DbContextOptions<PortalDbContext> options) : base(options)
        {

        }

        public override DbSet<User> Users { get; set; }

        public override DbSet<Role> Roles { get; set; }

        public DbSet<PermissionGroup> PermissionGroups { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<TimeOffRequest> TimeOffRequests { get; set; }

        public DbSet<NonCompliance> NonCompliances { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<ContractType> ContractTypes { get; set; }

        public DbSet<TimeOffRequestPanel> TimeOffRequestPanel { get; set; }
    }
}
