using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditEmployeeTrainingFeature
{
    public class EditEmployeeTrainingAction : Common.Action
    {
        public EditEmployeeTrainingAction(
            int id,
            int trainingId,
            string training,
            FileUploadResponse? file)
        {
            Id = id;
            TrainingId = trainingId;
            Training = training;
            File = file;
        }

        public int TrainingId { get; set; }

        public string Training { get; set; }

        public FileUploadResponse? File { get; set; }
    }
}
