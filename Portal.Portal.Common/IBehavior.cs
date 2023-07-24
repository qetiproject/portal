namespace Portal.Portal.Common
{
    public interface IBehavior<TEntity, TAction>
        where TEntity : IEntity
        where TAction : Action
    {
        public Type EntityType { get; }
        public Type ActionType { get; }
        public Result<TEntity> Behave(TEntity rootEntity, TAction Action);
    }
}
