using Microsoft.AspNetCore.Http;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditEmploymentHistoryFeature
{
    public class EditEmploymentHistoryAction : Common.Action
    {
        public EditEmploymentHistoryAction(
            int id,
            DateTime? jobStartDate,
            DateTime? contractTerm,
            string? supervisor,
            string? contractType)
        {
            Id = id;
            JobStartDate = jobStartDate;
            ContractTerm = contractTerm;
            Supervisor = supervisor;
            ContractType = contractType;
        }

        public DateTime? JobStartDate { get; set; }

        public DateTime? ContractTerm { get; set; }

        public string? Supervisor { get; set; }

        public string? ContractType { get; set; }
    }
}
