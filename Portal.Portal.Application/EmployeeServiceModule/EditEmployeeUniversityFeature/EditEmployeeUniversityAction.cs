using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditEmployeeUniversityFeature
{
    [Validate(typeof(EditEmployeeUniversityActionBusinessRules))]
    public class EditEmployeeUniversityAction : Common.Action
    {
        public EditEmployeeUniversityAction(
            int id,
            int universityId,
            string? university,
            string? faculty,
            DateTime startDate,
            DateTime endDate,
            FileUploadResponse? file)
        {
            Id = id;
            UniversityId = universityId;
            University = university;
            Faculty = faculty;
            StartDate = startDate;
            EndDate = endDate;
            File = file;
        }

        public int UniversityId { get; set; }

        public string University { get; set; }

        public string Faculty { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public FileUploadResponse? File { get; set; }
    }
}
