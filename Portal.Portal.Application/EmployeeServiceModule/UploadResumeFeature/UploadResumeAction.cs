using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.UploadResumeFeature
{
    public class UploadResumeAction : Common.Action
    {
        public UploadResumeAction(
            int id,
            FileUploadResponse? file)
        {
            Id = id;
            File = file;
        }

        public FileUploadResponse? File { get; set; }
    }
}
