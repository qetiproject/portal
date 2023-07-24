using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels
{
    public record EmployeeUniversity : Record
    {
        public EmployeeUniversity()
        {

        }

        public EmployeeUniversity(
            string university,
            string faculty,
            DateTime startDate,
            DateTime endDate,
            EmployeeFile? certificate)
        {
            University = university;
            Faculty = faculty;
            StartDate = startDate;
            EndDate = endDate;
            Certificate = certificate;
        }

        public string University { get; set; }

        public string Faculty { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public EmployeeFile? Certificate { get; set; }
    }
}
