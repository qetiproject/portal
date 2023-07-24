using Portal.Portal.Application.EmployeeServiceModule.EditEmploymentHistoryFeature;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.UploadBloodGroupAndRhesusFeature
{
    public class UploadBloodGroupAndRhesusBehavior : Behavior<Employee, UploadBloodGroupAndRhesusAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, UploadBloodGroupAndRhesusAction action)
        {
            rootEntity.OtherInformation ??= new OtherInformation();

            var otherInformation = rootEntity.OtherInformation;

            otherInformation.BloodGroupAndRhesus = action.File != null
                ? new EmployeeFile(action.File.FileName, action.File.FilePath, action.File.Type)
                : new EmployeeFile();

            return new Result<Employee>(rootEntity);
        }
    }
}
