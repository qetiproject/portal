using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.ApplicationUsers.CreateUserFeature
{
    public class CreateUserAction : Common.Action
    {
        public CreateUserAction(string email, string phoneNumber)
        {
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
