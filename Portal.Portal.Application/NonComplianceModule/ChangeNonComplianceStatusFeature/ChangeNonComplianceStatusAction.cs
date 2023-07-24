using Portal.Portal.Application.NonComplianceModule.Models.Enums;
using Portal.Portal.Application.NonComplianceModule.RedirectNonComplianceFeature;
using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.NonComplianceModule.ChangeNonComplianceStatusFeature
{
    [Validate(typeof(ChangeNonComplianceStatusActionBusinessRules))]
    public class ChangeNonComplianceStatusAction : Common.Action
    {
        public ChangeNonComplianceStatusAction(
            int id,
            string statusChanger,
            NonComplianceStatus status,
            string? comment,
            FileUploadResponse? file)
        {
            Id = id;
            StatusChanger = statusChanger;
            Status = status;
            Comment = comment;
            File = file;
        }

        public string StatusChanger { get; set; }

        public NonComplianceStatus Status { get; set; }

        public string? Comment { get; set; }

        public FileUploadResponse? File { get; set; }
    }
}
