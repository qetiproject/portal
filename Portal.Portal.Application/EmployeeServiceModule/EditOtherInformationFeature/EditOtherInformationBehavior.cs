using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditOtherInformationFeature
{
    public class EditOtherInformationBehavior : Behavior<Employee, EditOtherInformationAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, EditOtherInformationAction action)
        {
            rootEntity.OtherInformation ??= new OtherInformation();

            var otherInformation = rootEntity.OtherInformation;

            otherInformation.Gender = action.Gender;
            otherInformation.MaritalStatus = action.MaritalStatus;
            otherInformation.LegalAddress = action.LegalAddress;
            otherInformation.ActualAddress = action.ActualAddress;
            otherInformation.Conviction = action.Conviction;

            rootEntity.EmergencyContact ??= new EmergencyContact();

            var emergencyContact = rootEntity.EmergencyContact;

            emergencyContact.FullName = action.FullName;
            emergencyContact.Relationship = action.Relationship;
            emergencyContact.PhoneNumber = action.PhoneNumber;

            return new Result<Employee>(rootEntity);
        }
    }
}
