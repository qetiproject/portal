namespace Portal.Portal.Common
{
    public abstract class Behavior<TEntity, TAction> : IBehavior<IEntity, Action>
        where TEntity : IEntity
        where TAction : Action
    {
        private readonly Type entityType;
        private readonly Type actionType;

        public Behavior() : this(typeof(TEntity), typeof(TAction))
        {

        }

        public Behavior(Type entityType, Type actionType)
        {
            this.entityType = entityType;
            this.actionType = actionType;
        }

        public Type EntityType => entityType;

        public Type ActionType => actionType;

        public abstract Result<TEntity> Behave(TEntity rootEntity, TAction Action);

        public Result<IEntity> Behave(IEntity rootEntity, Action Action)
        {
            var nextState = Behave((TEntity)rootEntity, (TAction)Action);
            if (nextState.Ok)
                return new Result<IEntity>(nextState.Value);
            else
                return new Result<IEntity>(nextState.Errors.Messages);
        }
    }
}
