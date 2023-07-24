using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.DeleteEmployeeProfilePhotoFeature
{
    public class DeleteEmployeeProfilePhotoAction : Common.Action
    {
        public DeleteEmployeeProfilePhotoAction(int id)
        {
            Id = id;
        }
    }
}
