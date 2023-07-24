using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;

namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class OtherInformationRequest
    {
        public int Id { get; set; }
    }

    public class OtherInformationResponse
    {
        public int Id { get; set; }

        public OtherInformationModel OtherInformation { get; set; }

        public EmergencyContactModel EmergencyContact { get; set; }
    }

    public class OtherInformationModel
    {
        public OtherInformationModel(Gender? gender, MaritalStatus? maritalStatus, string? legalAddress, string? actualAddress, EmployeeFileModel? bloodGroupAndRhesus, EmployeeFileModel? medicalCertificate, EmployeeFileModel? alergies, EmployeeFileModel? drivingLicense, Conviction? conviction)
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

        public EmployeeFileModel? BloodGroupAndRhesus { get; set; }

        public EmployeeFileModel? MedicalCertificate { get; set; }

        public EmployeeFileModel? Alergies { get; set; }

        public EmployeeFileModel? DrivingLicense { get; set; }

        public Conviction? Conviction { get; set; }
    }

    public class EmergencyContactModel
    {
        public EmergencyContactModel(string? fullName, string? relationship, string? phoneNumber)
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
