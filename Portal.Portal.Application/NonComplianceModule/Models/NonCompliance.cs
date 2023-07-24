using Portal.Portal.Application.NonComplianceModule.Models.ChildModels;
using Portal.Portal.Application.NonComplianceModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.NonComplianceModule.Models
{
    public record NonCompliance : Record, IEntity
    {
        public NonCompliance()
        {
            Redirects = new HashSet<NonComplianceRedirect>();
        }

        public NonCompliance(
            string title,
            string sender,
            string violator,
            DateTime? approvalDeadline,
            decimal? fine,
            string description,
            NonComplianceFile? file) : this()
        {
            Title = title;
            Sender = sender;
            Violator = violator;
            Fine = fine;
            Description = description;
            File = file;
            Status = NonComplianceStatus.InProgress;
            CreateDate = DateTime.Now;
            ApprovalDeadline = approvalDeadline;
        }

        public string Title { get; set; }

        public string Sender { get; set; }

        public string Violator { get; set; }

        public DateTime? ApprovalDeadline { get; set; }

        public decimal? Fine { get; set; }

        public string Description { get; set; }

        public NonComplianceStatus Status { get; set; }

        public string? StatusChanger { get; set; }

        public string? StatusComment { get; set; }

        public DateTime? StatusChangeDate { get; set; }

        public NonComplianceFile? StatusFile { get; set; }

        public string? ViolatorComment { get; set; }

        public NonComplianceFile? ViolatorFile { get; set; }

        public DateTime CreateDate { get; set; }

        public HashSet<NonComplianceRedirect> Redirects { get; set; }

        public NonComplianceFile? File { get; set; }
    }
}
