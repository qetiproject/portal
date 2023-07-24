using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.RolesModule.DeleteEmployeeFeature
{
    public class DeleteEmployeeAction : Common.Action
    {
        public DeleteEmployeeAction(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }

        public int UserId { get; set; }
    }
}
