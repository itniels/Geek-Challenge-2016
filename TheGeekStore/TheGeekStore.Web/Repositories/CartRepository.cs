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
    public class CartRepository : IRepository<CartModel>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void Add(CartModel entity)
        {
            context.Carts.Add(entity);
            context.SaveChanges();
        }

        public void AddRange(IEnumerable<CartModel> entites)
        {
            context.Carts.AddRange(entites);
            context.SaveChanges();
        }

        public void Edit(CartModel entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Remove(CartModel entity)
        {
            context.Carts.Remove(entity);
            context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<CartModel> entites)
        {
            context.Carts.RemoveRange(entites);
            context.SaveChanges();
        }

        public void RemoveById(int id)
        {
            context.Carts.Remove(FindById(id));
            context.SaveChanges();
        }

        public IEnumerable<CartModel> GetAll()
        {
            return context.Carts.ToList();
        }

        public CartModel FindById(int id)
        {
            return context.Carts.Find(id);
        }

        public CartModel FindSingle(Expression<Func<CartModel, bool>> predicate)
        {
            return context.Set<CartModel>().Where(predicate).Single();
        }

        public IEnumerable<CartModel> FindAny(Expression<Func<CartModel, bool>> predicate)
        {
            return context.Set<CartModel>().Where(predicate);
        }

        public CartModel FindByUserId(string uid)
        {
            return context.Carts.SingleOrDefault(x => x.UserId == uid);
        }
    }
}