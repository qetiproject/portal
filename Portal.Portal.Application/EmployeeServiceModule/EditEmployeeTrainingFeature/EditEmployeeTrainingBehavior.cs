using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditEmployeeTrainingFeature
{
    public class EditEmployeeTrainingBehavior : Behavior<Employee, EditEmployeeTrainingAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, EditEmployeeTrainingAction action)
        {
            var training = rootEntity.Trainings.FirstOrDefault(u => u.Id == action.TrainingId);
            if (training is not null)
            {
                training.Training = action.Training;
                training.Certificate = action.File != null
                    ? new EmployeeFile(action.File.FileName, action.File.FilePath, action.File.Type)
                    : new EmployeeFile();
            }

            return new Result<Employee>(rootEntity);
        }
    }
}
