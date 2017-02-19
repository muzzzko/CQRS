namespace Domain.Repositories
{
    using System.Collections.Generic;
    using Entities;

    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity : IEntity
    {
        private readonly List<TEntity> _list = new List<TEntity>();



        public void Add(TEntity entity)
        {
            _list.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _list.Remove(entity);
        }

        public IEnumerable<TEntity> All()
        {
            return _list;
        }
    }
}
