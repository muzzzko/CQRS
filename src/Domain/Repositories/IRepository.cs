namespace Domain.Repositories
{
    using System.Collections.Generic;
    using Entities;

    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        void Add(TEntity entity);

        void Remove(TEntity entity);

        IEnumerable<TEntity> All();
    }
}
