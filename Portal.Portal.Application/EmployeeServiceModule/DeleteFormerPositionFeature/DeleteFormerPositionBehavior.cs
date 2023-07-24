using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.DeleteFormerPositionFeature
{
    public class DeleteFormerPositionBehavior : Behavior<Employee, DeleteFormerPositionAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, DeleteFormerPositionAction action)
        {
            var formerPosition = rootEntity.FormerPositions.FirstOrDefault(x => x.Id == action.FormerPositionId);

            if (formerPosition is not null)
                rootEntity.FormerPositions.Remove(formerPosition);

            return new Result<Employee>(rootEntity);
        }
    }
}
