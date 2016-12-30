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
    public class DailyDealRepository : IRepository<DailyDealModel>
    {
        private ApplicationDbContext context;

        public DailyDealRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(DailyDealModel entity)
        {
            context.DailyDeals.Add(entity);
            context.SaveChanges();
        }

        public void AddRange(IEnumerable<DailyDealModel> entites)
        {
            throw new NotImplementedException();
        }

        public void Edit(DailyDealModel entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(DailyDealModel entity)
        {
            context.DailyDeals.Remove(entity);
            context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<DailyDealModel> entites)
        {
            context.DailyDeals.RemoveRange(entites);
            context.SaveChanges();
        }

        public void RemoveById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DailyDealModel> GetAll()
        {
            return context.DailyDeals.Include(x => x.Product).ToList();
        }

        public DailyDealModel FindById(int id)
        {
            throw new NotImplementedException();
        }

        public DailyDealModel FindSingle(Expression<Func<DailyDealModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DailyDealModel> FindAny(Expression<Func<DailyDealModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}