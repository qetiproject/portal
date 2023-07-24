﻿using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.AdminPanelModule.Models
{
    public record Group : Record, IEntity
    {
        public Group()
        {

        }

        public Group(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
