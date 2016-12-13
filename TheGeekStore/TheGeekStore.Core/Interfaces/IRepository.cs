using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TheGeekStore.Core.Models;

namespace TheGeekStore.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entites);

        void Edit(TEntity entity);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entites);
        void RemoveById(int id);

        IEnumerable<TEntity> GetAll();
        TEntity FindById(int id);
        TEntity FindSingle(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> FindAny(Expression<Func<TEntity, bool>> predicate);
    }
}