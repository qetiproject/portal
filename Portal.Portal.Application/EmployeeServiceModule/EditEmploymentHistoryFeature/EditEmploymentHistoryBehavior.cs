using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditEmploymentHistoryFeature
{
    public class EditEmploymentHistoryBehavior : Behavior<Employee, EditEmploymentHistoryAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, EditEmploymentHistoryAction action)
        {
            rootEntity.EmploymentHistory ??= new EmploymentHistory();

            var employmentHistory = rootEntity.EmploymentHistory;

            employmentHistory.JobStartDate = action.JobStartDate;
            employmentHistory.ContractExpirationDate = action.ContractTerm;
            employmentHistory.Supervisor = action.Supervisor;
            employmentHistory.ContractType = action.ContractType;

            return new Result<Employee>(rootEntity);
        }
    }
}
