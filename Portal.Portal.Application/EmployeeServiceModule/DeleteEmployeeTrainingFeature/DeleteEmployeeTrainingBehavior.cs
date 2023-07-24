using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.DeleteEmployeeTrainingFeature
{
    public class DeleteEmployeeTrainingBehavior : Behavior<Employee, DeleteEmployeeTrainingAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, DeleteEmployeeTrainingAction action)
        {
            var training = rootEntity.Trainings.FirstOrDefault(u => u.Id == action.TrainingId);

            if (training is not null)
                rootEntity.Trainings.Remove(training);

            return new Result<Employee>(rootEntity);
        }
    }
}
