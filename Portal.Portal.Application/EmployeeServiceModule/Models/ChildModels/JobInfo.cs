using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels
{
    public record JobInfo : Record
    {
        public JobInfo()
        {

        }

        public JobInfo(
            string? region,
            string? workAddress,
            EmployeeFile? idDocument,
            DateTime? idExpirationDate,
            string? department,
            TimeZoneEnum? timeZone)
        {
            Region = region;
            WorkAddress = workAddress;
            IdDocument = idDocument;
            IdExpirationDate = idExpirationDate;
            Department = department;
            TimeZone = timeZone;
        }

        public string? Region { get; set; }

        public string? WorkAddress { get; set; }

        public EmployeeFile? IdDocument { get; set; }

        public DateTime? IdExpirationDate { get; set; }

        public string? Department { get; set; }

        public TimeZoneEnum? TimeZone { get; set; }
    }
}
