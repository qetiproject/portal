using Portal.Portal.Application.EmployeeServiceModule.CreateEmployeeFeature;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.AddEmployeeUniversityFeature
{
    [Validate(typeof(AddEmployeeUniversityActionBusinessRules))]
    public class AddEmployeeUniversityAction : Common.Action
    {
        public AddEmployeeUniversityAction(
            int id,
            string university,
            string faculty,
            DateTime startDate,
            DateTime endDate,
            FileUploadResponse? file)
        {
            Id = id;
            University = university;
            Faculty = faculty;
            StartDate = startDate;
            EndDate = endDate;
            File = file;
        }

        public string University { get; set; }

        public string Faculty { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public FileUploadResponse? File { get; set; }
    }
}
