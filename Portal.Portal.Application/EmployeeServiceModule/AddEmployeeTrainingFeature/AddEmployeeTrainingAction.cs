using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.AddEmployeeTrainingFeature
{
    public class AddEmployeeTrainingAction : Common.Action
    {
        public AddEmployeeTrainingAction(
            int id,
            string training,
            FileUploadResponse? file)
        {
            Id = id;
            Training = training;
            File = file;
        }

        public string Training { get; set; }

        public FileUploadResponse? File { get; set; }
    }
}
