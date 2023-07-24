namespace Portal.Portal.Common
{
    public interface INextState
    {
        Result<IEntity[]> Handle(IContent<IEntity> entityContent, Action action);
    }
}