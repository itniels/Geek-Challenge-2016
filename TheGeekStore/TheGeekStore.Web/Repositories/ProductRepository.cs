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
    public class ProductRepository : IRepository<ProductModel>, IProductRepository<ProductModel>
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public void Add(ProductModel entity)
        {
            context.Products.Add(entity);
        }

        public void AddRange(IEnumerable<ProductModel> entites)
        {
            context.Products.AddRange(entites);
        }

        public void Edit(ProductModel entity)
        {
            context.Entry(entity).State = EntityState.Modified;
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
            return context.Products;
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

        public ProductModel GetFeaturedProduct()
        {
            var rand = new Random();
            var items = GetFeaturedProducts().ToList();

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

        public IEnumerable<ProductModel> GetFeaturedProducts()
        {
            return context.Products.Where(x => x.Featured);
        }
    }
}