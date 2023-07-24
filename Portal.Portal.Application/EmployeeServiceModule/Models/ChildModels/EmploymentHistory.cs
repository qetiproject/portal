using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels
{
    public record EmploymentHistory : Record
    {
        public EmploymentHistory()
        {

        }

        public EmploymentHistory(DateTime? jobStartDate)
        {
            JobStartDate = jobStartDate;
        }

        public EmploymentHistory(
            string? contractType,
            DateTime? jobStartDate,
            int? employeeRoleId,
            EmployeeFile? contract,
            DateTime? contractExpirationDate,
            EmployeeFile? resume,
            string? supervisor)
        {
            ContractType = contractType;
            JobStartDate = jobStartDate;
            EmployeeRoleId = employeeRoleId;
            ContractExpirationDate = contractExpirationDate;
            Resume = resume;
            Contract = contract;
            Supervisor = supervisor;
        }

        public string? ContractType { get; set; }

        public DateTime? JobStartDate { get; set; }

        public int? EmployeeRoleId { get; set; }

        public EmployeeFile? Contract { get; set; }

        public DateTime? ContractExpirationDate { get; set; }

        public EmployeeFile? Resume { get; set; }

        public string? Supervisor { get; set; }
    }
}
