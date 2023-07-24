namespace Portal.Portal.Common
{
    public interface IContent<T>
        where T : IEntity
    {
        public T Get(Type entityType, object key);
        public Result<T[]> Store(T[] entities);
    }

}
