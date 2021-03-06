﻿using System;
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
    public class PurchaseRepository : IDisposable, IRepository<PurchaseModel>
    {
        private ApplicationDbContext context;

        public PurchaseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(PurchaseModel entity)
        {
            context.Purchases.Add(entity);
            context.SaveChanges();
        }

        public void AddRange(IEnumerable<PurchaseModel> entites)
        {
            context.Purchases.AddRange(entites);
            context.SaveChanges();
        }

        public void Edit(PurchaseModel entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Remove(PurchaseModel entity)
        {
            context.Purchases.Remove(entity);
            context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<PurchaseModel> entites)
        {
            context.Purchases.RemoveRange(entites);
            context.SaveChanges();
        }

        public void RemoveById(int id)
        {
            context.Purchases.Remove(FindById(id));
            context.SaveChanges();
        }

        public IEnumerable<PurchaseModel> GetAll()
        {
            return context.Purchases.OrderByDescending(x => x.PurchaseDate).ToList();
        }

        public PurchaseModel FindById(int id)
        {
            return context.Purchases.Find(id);
        }

        public IEnumerable<PurchaseModel> FindByUserId(string id)
        {
            return context.Purchases.Include(x => x.PurchaseItems).Where(x => x.UserId == id).OrderByDescending(x => x.PurchaseDate);
        }

        public PurchaseModel FindSingle(Expression<Func<PurchaseModel, bool>> predicate)
        {
            return context.Set<PurchaseModel>().Where(predicate).Single();
        }

        public IEnumerable<PurchaseModel> FindAny(Expression<Func<PurchaseModel, bool>> predicate)
        {
            return context.Set<PurchaseModel>().Where(predicate);
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