using Microsoft.AspNetCore.Http;
using Portal.Portal.Application.TimeOffRequestsModule.CreateTimeOffRequestFeature;
using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.NonComplianceModule.CreateNonComplianceFeature
{
    [Validate(typeof(CreateNonComplianceActionBusinessRules))]
    public class CreateNonComplianceAction : Common.Action
    {
        public CreateNonComplianceAction(
            string title,
            string sender,
            string violator,
            DateTime? approvalDeadline,
            decimal? fine,
            FileUploadResponse? file,
            string description)
        {
            Title = title;
            Sender = sender;
            Violator = violator;
            Fine = fine;
            File = file;
            Description = description;
            ApprovalDeadline = approvalDeadline;
        }

        public string Title { get; set; }

        public string Sender { get; set; }

        public string Violator { get; set; }

        public DateTime? ApprovalDeadline { get; set; }

        public decimal? Fine { get; set; }

        public FileUploadResponse? File { get; set; }

        public string Description { get; set; }
    }
}
