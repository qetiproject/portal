using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.NonComplianceModule.AddNonComplianceCommentFeature
{
    public class AddNonComplianceCommentAction : Common.Action
    {
        public AddNonComplianceCommentAction(
            int id,
            string comment,
            FileUploadResponse? file)
        {
            Id = id;
            Comment = comment;
            File = file;
        }

        public string Comment { get; set; }

        public FileUploadResponse? File { get; set; }
    }
}
