using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.DeleteEmployeeUniversityFeature
{
    public class DeleteEmployeeUniversityAction : Common.Action
    {
        public DeleteEmployeeUniversityAction(int id, int universityId)
        {
            Id = id;
            UniversityId = universityId;
        }

        public int UniversityId { get; set; }
    }
}
