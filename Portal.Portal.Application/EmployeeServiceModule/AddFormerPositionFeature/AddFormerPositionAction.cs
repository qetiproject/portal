using Portal.Portal.Application.EmployeeServiceModule.AddEmployeeUniversityFeature;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.AddFormerPositionFeature
{
    [Validate(typeof(AddFormerPositionActionBusinessRules))]
    public class AddFormerPositionAction : Common.Action
    {
        public AddFormerPositionAction(int id, string title, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Title = title;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
