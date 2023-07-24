using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.TimeOffRequestsModule.Models.ChildModels
{
    public record Redirect : Record
    {
        public Redirect()
        {

        }

        public Redirect(
            string redirecter,
            string participant,
            string comment,
            bool rightOfConfirmation,
            TimeOffRequestFile file
            )
        {
            Redirecter = redirecter;
            Participant = participant;
            Comment = comment;
            RightOfConfirmation = rightOfConfirmation;
            CreateDate = DateTime.Now;
            File = file;
        }

        public string Redirecter { get; set; }

        public string Participant { get; set; }

        public string? Comment { get; set; }

        public bool RightOfConfirmation { get; set; }

        public DateTime CreateDate { get; set; }

        public TimeOffRequestFile? File { get; set; }
    }
}
