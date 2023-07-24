using Portal.Portal.Application.EmployeeServiceModule.EditEmployeeJobInfoFeature;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.UpdateEmployeeProfilePhotoFeature
{
    public class UpdateEmployeeProfilePhotoBehavior : Behavior<Employee, UpdateEmployeeProfilePhotoAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, UpdateEmployeeProfilePhotoAction action)
        {
            rootEntity.Photo = new EmployeePhoto(action.Name, action.Path);

            return new Result<Employee>(rootEntity);
        }
    }
}
