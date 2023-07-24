using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels
{
    public record FormerPosition : Record
    {
        public FormerPosition()
        {

        }

        public FormerPosition(string title, DateTime startDate, DateTime endDate)
        {
            Title = title;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
