using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Common
{
    public class ActionHandler
    {
        private readonly INextState nextState;

        public ActionHandler(BusinessProcess businessProcess) : this(new NextState(businessProcess))
        {

        }

        public ActionHandler(IBehavior<IEntity, Action> behavior) : this(new NextState(behavior))
        {

        }

        public ActionHandler(INextState nextState)
        {
            this.nextState = nextState;
        }

        public Result<IEntity[]> Save(IContent<IEntity> entityStore, Action action)
        {
            var result = nextState.Handle(entityStore, action);

            if (result.Ok)
                return entityStore.Store(result.Value);
            else
                return result;
        }
    }
}
