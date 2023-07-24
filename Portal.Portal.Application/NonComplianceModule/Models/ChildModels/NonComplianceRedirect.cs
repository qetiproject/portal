using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.NonComplianceModule.Models.ChildModels
{
    public record NonComplianceRedirect : Record
    {
        public NonComplianceRedirect()
        {

        }

        public NonComplianceRedirect(
            string redirecter,
            string receiver,
            string comment,
            bool statusChange,
            NonComplianceFile? file
            )
        {
            Redirecter = redirecter;
            Receiver = receiver;
            Comment = comment;
            StatusChange = statusChange;
            CreateDate = DateTime.Now;
            File = file;
        }

        public string Redirecter { get; set; }

        public string Receiver { get; set; }

        public string? Comment { get; set; }

        public bool StatusChange { get; set; }

        public DateTime CreateDate { get; set; }

        public NonComplianceFile? File { get; set; }
    }
}
