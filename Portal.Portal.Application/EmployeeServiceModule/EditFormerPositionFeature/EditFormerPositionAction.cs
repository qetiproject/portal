using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditFormerPositionFeature
{
    [Validate(typeof(EditFormerPositionActionBusinessRules))]
    public class EditFormerPositionAction : Common.Action
    {
        public EditFormerPositionAction(int id, int formerPositionId, string title, DateTime startDate, DateTime endDate)
        {
            Id = id;
            FormerPositionId = formerPositionId;
            Title = title;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int FormerPositionId { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
