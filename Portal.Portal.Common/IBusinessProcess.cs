using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Common
{
    public interface IBusinessProcess
    {
        public IEntity[] Run(DbContext dbContext, Action action);
    }
}
