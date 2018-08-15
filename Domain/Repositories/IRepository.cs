using System.Collections.Generic;

namespace Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        T GetById(int id);

        IEnumerable<T> Get();

        void Remove(int id);

        void Save();

        void Update(T entity);

    }
}