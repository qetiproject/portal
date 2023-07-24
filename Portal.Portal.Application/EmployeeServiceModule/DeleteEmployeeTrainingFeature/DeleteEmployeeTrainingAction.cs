using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.DeleteEmployeeTrainingFeature
{
    public class DeleteEmployeeTrainingAction : Common.Action
    {
        public DeleteEmployeeTrainingAction(int id, int trainingId)
        {
            Id = id;
            TrainingId = trainingId;
        }

        public int TrainingId { get; set; }
    }
}
