using Microsoft.AspNetCore.Identity;
using Portal.Portal.Application.ApplicationUsers.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.ApplicationUsers.CreateUserFeature
{
    public class CreateUserBehavior : Behavior<User, CreateUserAction>
    {
        public UserManager<User> userManager { get; }

        public CreateUserBehavior(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public override Result<User> Behave(User rootEntity, CreateUserAction Action)
        {
            var user = new User()
            {
                Email = Action.Email,
                UserName = Action.Email,
                NormalizedEmail = userManager.NormalizeEmail(Action.Email),
                NormalizedUserName = userManager.NormalizeName(Action.Email),
                PhoneNumber = Action.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            //userManager.UpdateSecurityStampAsync(user);
            user.PasswordHash = userManager.PasswordHasher.HashPassword(user, "1234");

            return new Result<User>(user);
        }
    }
}
