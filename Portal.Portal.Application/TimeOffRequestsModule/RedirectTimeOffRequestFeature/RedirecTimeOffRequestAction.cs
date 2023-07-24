using Microsoft.AspNetCore.Http;
using Portal.Portal.Application.TimeOffRequestsModule.ChangeReceivedTimeOffRequestStatusFeature;
using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.TimeOffRequestsModule.RedirectTimeOffRequestFeature
{
    [Validate(typeof(RedirecTimeOffRequestActionBusinessRules))]
    public class RedirecTimeOffRequestAction : Common.Action
    {
        public RedirecTimeOffRequestAction(int id, string redirecter, string participant, FileUploadResponse? file, string? comment, bool rightOfConfirmation)
        {
            Id = id;
            Redirecter = redirecter;
            Participant = participant;
            File = file;
            Comment = comment;
            RightOfConfirmation = rightOfConfirmation;
        }

        public string Redirecter { get; set; }

        public string Participant { get; set; }

        public FileUploadResponse? File { get; set; }

        public string? Comment { get; set; }

        public bool RightOfConfirmation { get; set; }
    }
}
