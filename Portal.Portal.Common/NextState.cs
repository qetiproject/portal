using Microsoft.EntityFrameworkCore;

namespace Portal.Portal.Common
{
    public class NextState : INextState
    {
        public IBehavior<IEntity, Action> behavior;
        private readonly BusinessProcess businessProcess;
        private bool isBusinessProcess = false;

        public NextState(IBehavior<IEntity, Action> behavior)
        {
            this.behavior = behavior;
        }

        public NextState(BusinessProcess businessProcess)
        {
            this.businessProcess = businessProcess;
            isBusinessProcess = true;
        }

        public Result<IEntity[]> Handle(IContent<IEntity> entityContent, Action action)
        {
            try
            {
                if (isBusinessProcess)
                {
                    return businessProcess.Behave(entityContent, action);
                }
                else
                {
                    var validationResult = new ValidateAction(action).Validate();
                    if (!validationResult.IsValid)
                    {
                        return new Result<IEntity[]>(validationResult.Errors.Select(e => e.ErrorMessage).Distinct().ToArray());
                    }

                    var entity = entityContent.Get(behavior.EntityType, action.Id);

                    IEntity state = null;
                    if (entity != null)
                    {
                        state = entity;
                    }

                    var nextState = behavior.Behave(entity, action);
                    if (nextState.Ok)
                        return new Result<IEntity[]>(new[] { nextState.Value });
                    else
                        return new Result<IEntity[]>(nextState.Errors.Messages);
                }
            }
            catch (Exception ex)
            {
                return new Result<IEntity[]>(ex.InnerException.Message);
            }
        }
    }
}
