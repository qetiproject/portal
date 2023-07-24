using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.Models
{
    public record Employee : Record, IEntity
    {
        public Employee()
        {
            Photo = null;
            PayrollBasic = null;
            JobInfo = null;
            OtherInformation = null;
            EmploymentHistory = null;
            EmergencyContact = null;
            Trainings = new HashSet<EmployeeTraining>();
            Universities = new HashSet<EmployeeUniversity>();
            FormerPositions = new HashSet<FormerPosition>();
            ComputerSkills = new HashSet<ComputerSkill>();
            Languages = new HashSet<EmployeeLanguage>();
        }

        public Employee(PersonalInformation personalInformation) : this()
        {
            PersonalInformation = personalInformation;
        }

        public Employee(
            PersonalInformation personalInformation,
            PayrollBasic? payrollBasic,
            JobInfo? jobInfo,
            OtherInformation? otherInformation,
            EmploymentHistory? employmentHistory,
            EmergencyContact? emergencyContact)
        {
            PersonalInformation = personalInformation;
            PayrollBasic = payrollBasic;
            JobInfo = jobInfo;
            OtherInformation = otherInformation;
            EmploymentHistory = employmentHistory;
            EmergencyContact = emergencyContact;
            Photo = null;
            Trainings = new HashSet<EmployeeTraining>();
            Universities = new HashSet<EmployeeUniversity>();
            FormerPositions = new HashSet<FormerPosition>();
            ComputerSkills = new HashSet<ComputerSkill>();
            Languages = new HashSet<EmployeeLanguage>();
        }

        public PersonalInformation PersonalInformation { get; set; }

        public EmployeePhoto? Photo { get; set; }

        public PayrollBasic? PayrollBasic { get; set; }

        public JobInfo? JobInfo { get; set; }

        public HashSet<EmployeeTraining> Trainings { get; set; }

        public HashSet<EmployeeUniversity> Universities { get; set; }

        public OtherInformation? OtherInformation { get; set; }

        public EmergencyContact? EmergencyContact { get; set; }

        public EmploymentHistory? EmploymentHistory { get; set; }

        public HashSet<FormerPosition> FormerPositions { get; set; }

        public HashSet<ComputerSkill> ComputerSkills { get; set; }

        public HashSet<EmployeeLanguage> Languages { get; set; }
    }
}
