using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels
{
    public record EmployeeTraining : Record
    {
        public EmployeeTraining()
        {

        }

        public EmployeeTraining(
            string training,
            EmployeeFile? certificate)
        {
            Training = training;
            Certificate = certificate;
        }

        public string Training { get; set; }

        public EmployeeFile? Certificate { get; set; }
    }
}
