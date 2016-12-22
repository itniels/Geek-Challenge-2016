using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Microsoft.Ajax.Utilities;
using TheGeekStore.Core.Interfaces;
using TheGeekStore.Core.Models;
using TheGeekStore.Models;

namespace TheGeekStore.Repositories
{
    public class CartRepository : IDisposable, IRepository<CartModel>
    {
        private ApplicationDbContext context;

        public CartRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

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
            entity.CartItems.Clear();
            context.Carts.Remove(entity);
            context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<CartModel> entites)
        {
            foreach (CartModel cart in entites)
            {
                cart.CartItems.Clear();
            }
            context.Carts.RemoveRange(entites);
            context.SaveChanges();
        }

        public void RemoveById(int id)
        {
            CartModel cart = FindById(id);
            cart.CartItems.Clear();
            context.Carts.Remove(cart);
            context.SaveChanges();
        }

        public IEnumerable<CartModel> GetAll()
        {
            return context.Carts.ToList();
        }

        public CartModel FindById(int id)
        {
            return (from s in context.Carts.Include(x => x.CartItems)
                    where s.Id == id
                    where s.UserId == null
                    select s)
                    .FirstOrDefault();
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
            return context.Carts.Include(x => x.CartItems).SingleOrDefault(x => x.UserId == uid);
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