using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.EmployeeServiceModule.UploadContractFeature;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.UploadIdDocumentFeature
{
    public class UploadIdDocumentBehavior : Behavior<Employee, UploadIdDocumentAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, UploadIdDocumentAction action)
        {
            rootEntity.JobInfo ??= new JobInfo();

            var jobInfo = rootEntity.JobInfo;

            jobInfo.IdDocument = action.File != null
                ? new EmployeeFile(action.File.FileName, action.File.FilePath, action.File.Type)
                : new EmployeeFile();

            return new Result<Employee>(rootEntity);
        }
    }
}
