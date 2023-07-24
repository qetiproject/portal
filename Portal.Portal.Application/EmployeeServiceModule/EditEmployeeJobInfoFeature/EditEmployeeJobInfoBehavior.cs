using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditEmployeeJobInfoFeature
{
    public class EditEmployeeJobInfoBehavior : Behavior<Employee, EditEmployeeJobInfoAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, EditEmployeeJobInfoAction action)
        {
            rootEntity.JobInfo ??= new JobInfo();

            var jobInfo = rootEntity.JobInfo;

            jobInfo.Region = action.Region;
            jobInfo.WorkAddress = action.WorkAddress;
            jobInfo.IdExpirationDate = action.IdExpirationDate;
            jobInfo.Department = action.Department;
            jobInfo.TimeZone = action.TimeZone;

            //var jobInfo = new JobInfo(
            //    action.Region,
            //    action.WorkAddress,
            //    action.EmployeeId,
            //    action.IdExpirationDate,
            //    action.Department,
            //    action.TimeZone);

            //rootEntity = rootEntity with { JobInfo = jobInfo };

            return new Result<Employee>(rootEntity);
        }
    }
}
