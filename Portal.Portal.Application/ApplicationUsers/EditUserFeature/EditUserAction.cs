using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.ApplicationUsers.EditUserFeature
{
    public class EditUserAction : Common.Action
    {
        public EditUserAction(int id, string email, string phoneNumber)
        {
            Id = id;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
