using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;

namespace Portal.Portal.Application.EmployeeServiceModule.AddFormerPositionFeature
{
    public class AddFormerPositionBehavior : Behavior<Employee, AddFormerPositionAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, AddFormerPositionAction action)
        {
            var formerPosition = new FormerPosition(
                action.Title,
                action.StartDate,
                action.EndDate
                );

            rootEntity.FormerPositions.Add(formerPosition);

            return new Result<Employee>(rootEntity);
        }
    }
}
