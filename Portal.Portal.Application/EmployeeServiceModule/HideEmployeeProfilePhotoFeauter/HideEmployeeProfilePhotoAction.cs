using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.HideEmployeeProfilePhotoFeauter
{
    public class HideEmployeeProfilePhotoAction : Common.Action
    {
        public HideEmployeeProfilePhotoAction(
            int id,
            bool hidden)
        {
            Id = id;
            Hidden = hidden;
        }

        public bool Hidden { get; set; }
    }
}
