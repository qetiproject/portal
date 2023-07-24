using Portal.Portal.Application.NonComplianceModule.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.NonCompliance.Models
{
    public class SentNonComplianceDetailsRequest
    {
        [Required]
        public string Number { get; set; }
    }

    public class SentNonComplianceDetailsResponse
    {
        public SentNonComplianceDetailsResponse(
            string number,
            string group,
            string violator,
            DateTime? approvalDeadline,
            DateTime date,
            decimal? fine,
            NonComplianceFileModel? file,
            string? description,
            string? violatorComment,
            NonComplianceFileModel? violatorFile,
            NonComplianceStatus status,
            string? statusChanger,
            string? statusComment,
            DateTime? statusChangeDate,
            NonComplianceFileModel? statusFile,
            List<NonComplianceRedirectModel> redirects)
        {
            Number = number;
            Group = group;
            Violator = violator;
            Date = date;
            Fine = fine;
            File = file;
            Description = description;
            ViolatorComment = violatorComment;
            ViolatorFile = violatorFile;
            Status = status;
            StatusChanger = statusChanger;
            StatusComment = statusComment;
            StatusFile = statusFile;
            Redirects = redirects;
            StatusChangeDate = statusChangeDate;
            ApprovalDeadline = approvalDeadline;
        }

        public string Number { get; set; }

        public string Group { get; set; }

        public string Violator { get; set; }

        public DateTime? ApprovalDeadline { get; set; }

        public DateTime Date { get; set; }

        public decimal? Fine { get; set; }

        public NonComplianceFileModel? File { get; set; }

        public string? Description { get; set; }

        public string? ViolatorComment { get; set; }

        public NonComplianceFileModel? ViolatorFile { get; set; }

        public NonComplianceStatus Status { get; set; }

        public string? StatusChanger { get; set; }

        public string? StatusComment { get; set; }

        public DateTime? StatusChangeDate { get; set; }

        public NonComplianceFileModel? StatusFile { get; set; }

        public List<NonComplianceRedirectModel> Redirects { get; set; }
    }
}
