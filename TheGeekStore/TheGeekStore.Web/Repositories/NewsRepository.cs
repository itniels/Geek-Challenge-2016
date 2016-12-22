using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TheGeekStore.Core.Interfaces;
using TheGeekStore.Core.Models;
using TheGeekStore.Models;

namespace TheGeekStore.Repositories
{
    public class NewsRepository : IDisposable, IRepository<NewsModel>
    {
        private ApplicationDbContext context;

        public NewsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(NewsModel entity)
        {
            context.News.Add(entity);
        }

        public void AddRange(IEnumerable<NewsModel> entites)
        {
            context.News.AddRange(entites);
        }

        public void Edit(NewsModel entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(NewsModel entity)
        {
            context.News.Remove(entity);
        }

        public void RemoveRange(IEnumerable<NewsModel> entites)
        {
            context.News.RemoveRange(entites);
        }

        public void RemoveById(int id)
        {
            context.News.Remove(FindById(id));
            context.SaveChanges();
        }

        public IEnumerable<NewsModel> GetAll()
        {
            return context.News.OrderByDescending(x => x.Time);
        }

        public NewsModel FindById(int id)
        {
            return context.News.Find(id);
        }

        public NewsModel FindSingle(Expression<Func<NewsModel, bool>> predicate)
        {
            return context.Set<NewsModel>().Where(predicate).Single();
        }

        public IEnumerable<NewsModel> FindAny(Expression<Func<NewsModel, bool>> predicate)
        {
            return context.Set<NewsModel>().Where(predicate);
        }

        public IEnumerable<NewsModel> GetLatest3()
        {
            return (from t in context.News
                    orderby t.Time descending
                    select t).Take(3);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}