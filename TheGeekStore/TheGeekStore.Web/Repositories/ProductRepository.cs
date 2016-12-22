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
    public class ProductRepository : IDisposable, IRepository<ProductModel>
    {
        private ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(ProductModel entity)
        {
            context.Products.Add(entity);
            context.SaveChanges();
        }

        public void AddRange(IEnumerable<ProductModel> entites)
        {
            context.Products.AddRange(entites);
            context.SaveChanges();
        }

        public void Edit(ProductModel entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Remove(ProductModel entity)
        {
            context.Products.Remove(entity);
            context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<ProductModel> entites)
        {
            context.Products.RemoveRange(entites);
            context.SaveChanges();
        }

        public void RemoveById(int id)
        {
            context.Products.Remove(FindById(id));
            context.SaveChanges();
        }

        public IEnumerable<ProductModel> GetAll()
        {
            return context.Products.Include(x => x.Category);
        }

        public ProductModel FindById(int id)
        {
            return context.Products.Find(id);
        }

        public ProductModel FindSingle(Expression<Func<ProductModel, bool>> predicate)
        {
            return context.Set<ProductModel>().Where(predicate).Single();
        }

        public IEnumerable<ProductModel> FindAny(Expression<Func<ProductModel, bool>> predicate)
        {
            return context.Set<ProductModel>().Where(predicate);
        }

        public IEnumerable<ProductModel> GetTop3Sold()
        {
            return (from t in context.Products
                    orderby t.TimesPuchased
                    select t).Take(3);
        }

        public IEnumerable<ProductModel> GetFeaturedProducts()
        {
            return context.Products.Where(x => x.Featured);
        }


        public ProductModel GetFeaturedProduct()
        {
            var rand = new Random();
            List<ProductModel> items = GetFeaturedProducts().ToList();
            if (items.Count == 0)
                return null;

            ProductModel model = null;
            var i = 20;
            while (model == null || i <= 0)
            {
                int u = rand.Next(0, items.Count);
                model = items[u];
                i--;
            }

            return model ?? (model = context.Products.First());
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