using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Portal.Portal.Application.ApplicationRoles.Models;
using Portal.Portal.Application.ApplicationUsers.Models;
using Portal.Portal.Application.RolesModule.Models.ChildModels;

namespace Portal.Portal.Persistence
{
    public static class Seed
    {
        public static void Initialize(PortalDbContext db, IConfiguration configuration, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            InitialPermissions(db, configuration);

            var roles = db.Roles.ToList();

            var permissions = db.PermissionGroups.AsNoTracking().SelectMany(p => p.Permissions).ToList();

            var adminRole = db.Roles.FirstOrDefault(r => r.Name == ("Admin"));
            if (adminRole is null)
            {
                adminRole = new Role("Admin", "Administrator's Role");
                roleManager.UpdateNormalizedRoleNameAsync(adminRole);

                foreach (var permission in permissions)
                {
                    adminRole.Permissions.Add(new RolePermission(
                        permission.Id,
                        permission.PermissionKey,
                        permission.Description
                    ));
                }

                db.Add(adminRole);
            }
            else
            {
                UpdateAdminPermissionsIfNecessary(db);
            }

            var employeeRole = db.Roles.FirstOrDefault(r => r.Name == ("Employee"));
            if (employeeRole is null)
            {
                employeeRole = new Role("Employee", "Employee's Role");
                roleManager.UpdateNormalizedRoleNameAsync(employeeRole);

                foreach (var permission in permissions.Where(p => p.PermissionKey == "ViewEmployeesList"))
                {
                    employeeRole.Permissions.Add(new RolePermission(
                        permission.Id,
                        permission.PermissionKey,
                        permission.Description
                    ));
                }

                db.Add(employeeRole);
            }

            db.SaveChanges();

            //var users = db.Set<ApplicationUser>().ToList();

            //if (users == null || !users.Any())
            //{
            //    var role = roleManager.FindByNameAsync("Admin").Result;

            //    var user = new ApplicationUser()
            //    {
            //        Email = "admin@gmail.com",
            //        UserName = "Administrator",
            //        NormalizedEmail = userManager.NormalizeEmail("admin@gmail.com"),
            //        NormalizedUserName = userManager.NormalizeName("Administrator"),
            //    };

            //    userManager.UpdateSecurityStampAsync(user);
            //    user.PasswordHash = userManager.PasswordHasher.HashPassword(user, "admin1234");
            //    //userManager.CreateAsync(user, "admin1234");

            //    db.Add(user);

            //    db.SaveChanges();

            //    user.Roles.Add(new ApplicationUserRole
            //    {
            //        RoleId = role.Id,
            //        UserId = user.Id
            //    });

            //    db.SaveChanges();
            //}
        }

        public static void UpdateAdminPermissionsIfNecessary(PortalDbContext db)
        {
            var adminRole = db.Roles.Include(x => x.Permissions).FirstOrDefault(x => x.Name == "Admin");
            var permissions = db.PermissionGroups.AsNoTracking().SelectMany(p => p.Permissions).ToList();
            var newPermissions = new List<Permission>();
            foreach (var permission in permissions)
            {
                if (!adminRole.Permissions.Select(p => p.PermissionKey).Any(p => p == permission.PermissionKey))
                {
                    newPermissions.Add(permission);
                }
            }

            foreach (var permission in newPermissions)
            {
                adminRole.Permissions.Add(new RolePermission(
                    permission.Id,
                    permission.PermissionKey,
                    permission.Description
                ));
            }

            db.SaveChanges();
        }

        public static void InitialPermissions(PortalDbContext db, IConfiguration configuration)
        {
            var configSection = configuration.GetSection("PermissionConfig");
            foreach (var section in configSection.GetChildren())
            {
                var permissionGroup = db.PermissionGroups.Include(p => p.Permissions).FirstOrDefault(g => g.Name == section.Key);

                if (permissionGroup is null)
                    permissionGroup = new PermissionGroup(section.Key);

                foreach (var sectionItem in section.GetChildren()
                                                   .Select(x => new { Key = x.GetSection("Key").Value, Description = x.GetSection("Description").Value })
                                                   .ToList())
                {
                    if (!permissionGroup.Permissions.Select(p => p.PermissionKey).Contains(sectionItem.Key))
                    {
                        permissionGroup.Permissions.Add(new Permission()
                        {
                            PermissionKey = sectionItem.Key,
                            Description = sectionItem.Description
                        });
                    }
                }

                db.Update(permissionGroup);
            }

            db.SaveChanges();
        }
    }
}

