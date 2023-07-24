using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.AddEmployeeTrainingFeature
{
    public class AddEmployeeTrainingBehavior : Behavior<Employee, AddEmployeeTrainingAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, AddEmployeeTrainingAction action)
        {
            var file = action.File != null
                ? new EmployeeFile(action.File.FileName, action.File.FilePath, action.File.Type)
                : new EmployeeFile();

            var training = new EmployeeTraining(
                action.Training,
                file
                );

            rootEntity.Trainings.Add(training);

            return new Result<Employee>(rootEntity);
        }
    }
}
