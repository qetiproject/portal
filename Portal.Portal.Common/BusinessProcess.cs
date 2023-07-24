namespace Portal.Portal.Common
{
    public abstract class BusinessProcess
    {
        public abstract Result<IEntity[]> Behave(IContent<IEntity> entityContent, Action action);
    }

}
