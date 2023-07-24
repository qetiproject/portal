using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.EmployeeServiceModule.UpdateEmployeeProfilePhotoFeature;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.DeleteEmployeeProfilePhotoFeature
{
    public class DeleteEmployeeProfilePhotoBehavior : Behavior<Employee, DeleteEmployeeProfilePhotoAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, DeleteEmployeeProfilePhotoAction action)
        {
            rootEntity.Photo = null;

            return new Result<Employee>(rootEntity);
        }
    }
}
