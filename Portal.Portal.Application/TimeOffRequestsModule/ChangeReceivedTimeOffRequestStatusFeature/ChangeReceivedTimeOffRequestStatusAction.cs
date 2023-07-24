using Microsoft.AspNetCore.Http;
using Portal.Portal.Application.EmployeeServiceModule.AddFormerPositionFeature;
using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.TimeOffRequestsModule.ChangeReceivedTimeOffRequestStatusFeature
{
    [Validate(typeof(ChangeReceivedTimeOffRequestStatusActionBusinessRules))]
    public class ChangeReceivedTimeOffRequestStatusAction : Common.Action
    {
        public ChangeReceivedTimeOffRequestStatusAction(
            int id,
            string statusChanger,
            TimeOffRequestStatus status,
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

        public TimeOffRequestStatus Status { get; set; }

        public string? Comment { get; set; }

        public FileUploadResponse? File { get; set; }
    }
}
