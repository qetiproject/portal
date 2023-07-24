using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.AdminPanelModule.CreateContractTypeFeature
{
    [Validate(typeof(CreateContractTypeActionBusinessRules))]
    public class CreateContractTypeAction : Common.Action
    {
        public CreateContractTypeAction(string contractType)
        {
            ContractType = contractType;
        }

        public string ContractType { get; set; }
    }
}
