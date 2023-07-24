using Portal.Portal.Application.AdminPanelModule.Models;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.AdminPanelModule.CreateContractTypeFeature
{
    public class CreateContractTypeBehavior : Behavior<ContractType, CreateContractTypeAction>
    {
        public override Result<ContractType> Behave(ContractType rootEntity, CreateContractTypeAction action)
        {
            var contractType = new ContractType(action.ContractType);

            return new Result<ContractType>(contractType);
        }
    }
}
