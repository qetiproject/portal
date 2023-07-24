using Portal.Portal.Application.EmployeeServiceModule.DeleteEmployeeProfilePhotoFeature;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.HideEmployeeProfilePhotoFeauter
{
    public class HideEmployeeProfilePhotoBehavior : Behavior<Employee, HideEmployeeProfilePhotoAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, HideEmployeeProfilePhotoAction action)
        {
            var photo = rootEntity.Photo;
            if (photo != null)
            {
                photo.Hidden = action.Hidden;
            }

            return new Result<Employee>(rootEntity);
        }
    }
}
