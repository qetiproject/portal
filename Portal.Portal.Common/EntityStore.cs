using Microsoft.EntityFrameworkCore;

namespace Portal.Portal.Common
{
    public class EntityStore : IContent<IEntity>
    {
        private readonly DbContext dbContext;

        public EntityStore(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEntity Get(Type entityType, object key)
        {
            var entity = (IEntity)dbContext.Find(entityType, key);

            if (entity is not null)
            {
                foreach (var reference in dbContext.Entry(entity).References)
                {
                    reference.Load();
                }

                foreach (var collection in dbContext.Entry(entity).Collections)
                {
                    collection.Load();
                }

                //dbContext.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public Result<IEntity[]> Store(IEntity[] entities)
        {
            try
            {
                var result = entities.Select(entity =>
                {
                    //var entry = dbContext.Entry(entity);

                    //if (entry.IsKeySet)
                    //{
                    //    entry.State = EntityState.Modified;
                    //}
                    //else
                    //{
                    //    entry.State = EntityState.Added;
                    //}
                    dbContext.ChangeTracker.TrackGraph(entity, e =>
                    {
                        if (e.Entry.IsKeySet)
                        {
                            e.Entry.State = EntityState.Modified;
                        }
                        else
                        {
                            e.Entry.State = EntityState.Added;
                        }
                    });
                    return true;
                }).ToList();

                dbContext.SaveChanges();

                return new Result<IEntity[]>(entities);
            }
            catch (Exception ex)
            {
                return new Result<IEntity[]>(ex.InnerException.Message);
            }
        }
    }
}
