using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditEmployeeJobInfoFeature
{
    public class EditEmployeeJobInfoAction : Common.Action
    {
        public EditEmployeeJobInfoAction(
            int id,
            string? region,
            string? workAddress,
            DateTime? idExpirationDate,
            string? department,
            TimeZoneEnum? timeZone)
        {
            Id = id;
            Region = region;
            WorkAddress = workAddress;
            IdExpirationDate = idExpirationDate;
            Department = department;
            TimeZone = timeZone;
        }

        public string? Region { get; set; }

        public string? WorkAddress { get; set; }

        public DateTime? IdExpirationDate { get; set; }

        public string? Department { get; set; }

        public TimeZoneEnum? TimeZone { get; set; }
    }
}
