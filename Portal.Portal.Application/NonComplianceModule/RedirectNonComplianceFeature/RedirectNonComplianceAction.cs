using Microsoft.AspNetCore.Http;
using Portal.Portal.Application.NonComplianceModule.CreateNonComplianceFeature;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.NonComplianceModule.RedirectNonComplianceFeature
{
    [Validate(typeof(RedirectNonComplianceActionBusinessRules))]
    public class RedirectNonComplianceAction : Common.Action
    {
        public RedirectNonComplianceAction(
            int id,
            string redirecter,
            string receiver,
            FileUploadResponse? file,
            string? comment,
            bool statusChange)
        {
            Id = id;
            Redirecter = redirecter;
            Receiver = receiver;
            File = file;
            Comment = comment;
            StatusChange = statusChange;
        }

        public string Redirecter { get; set; }

        public string Receiver { get; set; }

        public FileUploadResponse? File { get; set; }

        public string? Comment { get; set; }

        public bool StatusChange { get; set; }
    }
}
