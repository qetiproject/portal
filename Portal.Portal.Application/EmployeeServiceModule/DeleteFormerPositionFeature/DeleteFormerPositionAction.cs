using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.DeleteFormerPositionFeature
{
    public class DeleteFormerPositionAction : Common.Action
    {
        public DeleteFormerPositionAction(int id, int formerPositionId)
        {
            Id = id;
            FormerPositionId = formerPositionId;
        }

        public int FormerPositionId { get; set; }
    }
}
