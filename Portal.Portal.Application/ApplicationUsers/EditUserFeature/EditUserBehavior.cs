using Microsoft.AspNetCore.Identity;
using Portal.Portal.Application.ApplicationUsers.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.ApplicationUsers.EditUserFeature
{
    public class EditUserBehavior : Behavior<User, EditUserAction>
    {
        public UserManager<User> userManager { get; }

        public EditUserBehavior(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public override Result<User> Behave(User rootEntity, EditUserAction Action)
        {
            rootEntity.Email = Action.Email;
            rootEntity.UserName = Action.Email;
            rootEntity.NormalizedEmail = userManager.NormalizeEmail(Action.Email);
            rootEntity.NormalizedUserName = userManager.NormalizeName(Action.Email);
            rootEntity.PhoneNumber = Action.PhoneNumber;

            return new Result<User>(rootEntity);
        }
    }
}
