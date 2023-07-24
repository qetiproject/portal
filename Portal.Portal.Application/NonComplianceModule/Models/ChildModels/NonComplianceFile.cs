using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.NonComplianceModule.Models.ChildModels
{
    public record NonComplianceFile : Record
    {
        public NonComplianceFile()
        {

        }

        public NonComplianceFile(
            string? name,
            string? path,
            FileType? type
            )
        {
            Name = name;
            Path = path;
            Type = type;
        }

        public string? Name { get; set; }

        public string? Path { get; set; }

        public FileType? Type { get; set; }
    }
}
