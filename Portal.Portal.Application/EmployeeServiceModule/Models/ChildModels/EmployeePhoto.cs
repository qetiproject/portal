using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels
{
    public record EmployeePhoto : Record
    {
        public EmployeePhoto()
        {
            Name = string.Empty;
            Path = string.Empty;
            Hidden = false;
        }

        public EmployeePhoto(string name, string path)
        {
            Name = name;
            Path = path;
            Hidden = false;
        }

        public string Name { get; set; }

        public string Path { get; set; }

        public bool Hidden { get; set; }
    }
}
