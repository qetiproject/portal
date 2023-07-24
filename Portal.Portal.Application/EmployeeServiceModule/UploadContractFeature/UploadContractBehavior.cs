using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.EmployeeServiceModule.UploadAlergiesFeature;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.UploadContractFeature
{
    public class UploadContractBehavior : Behavior<Employee, UploadContractAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, UploadContractAction action)
        {
            rootEntity.EmploymentHistory ??= new EmploymentHistory();

            var employmentHistory = rootEntity.EmploymentHistory;

            employmentHistory.Contract = action.File != null
                ? new EmployeeFile(action.File.FileName, action.File.FilePath, action.File.Type)
                : new EmployeeFile();

            return new Result<Employee>(rootEntity);
        }
    }
}
