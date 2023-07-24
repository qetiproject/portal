using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.UpdateEmployeeProfilePhotoFeature
{
    public class UpdateEmployeeProfilePhotoAction : Common.Action
    {
        public UpdateEmployeeProfilePhotoAction(int id, string name, string path)
        {
            Id = id;
            Name = name;
            Path = path;
        }

        public string Name { get; set; }

        public string Path { get; set; }
    }
}
