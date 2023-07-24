using Portal.Portal.Application.NonComplianceModule.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.NonCompliance.Models
{
    public class MyNonComplianceDetailsRequest
    {
        [Required]
        public string Number { get; set; }
    }

    public class MyNonComplianceDetailsResponse
    {
        public MyNonComplianceDetailsResponse(
            string number,
            string group,
            string sender,
            DateTime? approvalDeadline,
            DateTime date,
            decimal? fine,
            NonComplianceFileModel? file,
            string? description,
            string? myComment,
            NonComplianceFileModel? myFile,
            NonComplianceStatus status,
            string? statusChanger,
            string? statusComment,
            DateTime? statusChangeDate,
            NonComplianceFileModel? statusFile,
            List<NonComplianceRedirectModel> redirects)
        {
            Number = number;
            Group = group;
            Sender = sender;
            Date = date;
            Fine = fine;
            File = file;
            Description = description;
            MyComment = myComment;
            MyFile = myFile;
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

        public string Sender { get; set; }

        public DateTime? ApprovalDeadline { get; set; }

        public DateTime Date { get; set; }

        public decimal? Fine { get; set; }

        public NonComplianceFileModel? File { get; set; }

        public string? Description { get; set; }

        public string? MyComment { get; set; }

        public NonComplianceFileModel? MyFile { get; set; }

        public NonComplianceStatus Status { get; set; }

        public string? StatusChanger { get; set; }

        public string? StatusComment { get; set; }

        public DateTime? StatusChangeDate { get; set; }

        public NonComplianceFileModel? StatusFile { get; set; }

        public List<NonComplianceRedirectModel> Redirects { get; set; }
    }
}
