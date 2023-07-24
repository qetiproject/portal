using Portal.Portal.Application.EmployeeServiceModule.EditEmploymentHistoryFeature;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.UploadAlergiesFeature
{
    public class UploadAlergiesBehavior : Behavior<Employee, UploadAlergiesAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, UploadAlergiesAction action)
        {
            rootEntity.OtherInformation ??= new OtherInformation();

            var otherInformation = rootEntity.OtherInformation;

            otherInformation.Alergies = action.File != null
                ? new EmployeeFile(action.File.FileName, action.File.FilePath, action.File.Type)
                : new EmployeeFile();

            return new Result<Employee>(rootEntity);
        }
    }
}
