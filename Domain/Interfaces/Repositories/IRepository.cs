using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {

        IQueryable<TEntity> Queryable();

        TEntity GetById(int id);

        IEnumerable<TEntity> GetAll();

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(int id);

    }
}