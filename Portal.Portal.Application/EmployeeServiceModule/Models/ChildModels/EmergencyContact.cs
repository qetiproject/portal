using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels
{
    public record EmergencyContact : Record
    {
        public EmergencyContact()
        {

        }

        public EmergencyContact(string fullName, string relationship, string phoneNumber)
        {
            FullName = fullName;
            Relationship = relationship;
            PhoneNumber = phoneNumber;
        }

        public string? FullName { get; set; }

        public string? Relationship { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
