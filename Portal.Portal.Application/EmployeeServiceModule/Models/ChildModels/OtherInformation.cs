using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels
{
    public record OtherInformation : Record
    {
        public OtherInformation()
        {

        }
        public OtherInformation(
            Gender? gender,
            MaritalStatus? maritalStatus,
            string? legalAddress,
            string? actualAddress,
            EmployeeFile? bloodGroupAndRhesus,
            EmployeeFile? medicalCertificate,
            EmployeeFile? alergies,
            EmployeeFile? drivingLicense,
            Conviction? conviction)
        {
            Gender = gender;
            MaritalStatus = maritalStatus;
            LegalAddress = legalAddress;
            ActualAddress = actualAddress;
            BloodGroupAndRhesus = bloodGroupAndRhesus;
            MedicalCertificate = medicalCertificate;
            Alergies = alergies;
            DrivingLicense = drivingLicense;
            Conviction = conviction;
        }

        public Gender? Gender { get; set; }

        public MaritalStatus? MaritalStatus { get; set; }

        public string? LegalAddress { get; set; }

        public string? ActualAddress { get; set; }

        public EmployeeFile? BloodGroupAndRhesus { get; set; }

        public EmployeeFile? MedicalCertificate { get; set; }

        public EmployeeFile? Alergies { get; set; }

        public EmployeeFile? DrivingLicense { get; set; }

        public Conviction? Conviction { get; set; }
    }
}
