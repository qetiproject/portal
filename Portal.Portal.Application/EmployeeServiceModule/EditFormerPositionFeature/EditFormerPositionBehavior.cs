using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditFormerPositionFeature
{
    public class EditFormerPositionBehavior : Behavior<Employee, EditFormerPositionAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, EditFormerPositionAction action)
        {
            var formerPosition = rootEntity.FormerPositions.FirstOrDefault(x => x.Id == action.FormerPositionId);
            if (formerPosition is not null)
            {
                formerPosition.Title = action.Title;
                formerPosition.StartDate = action.StartDate;
                formerPosition.EndDate = action.EndDate;
            }

            return new Result<Employee>(rootEntity);
        }
    }
}
