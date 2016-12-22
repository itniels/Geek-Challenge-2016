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
    public class CustomerProfileRepository : IRepository<CustomerProfileModel>
    {
        private ApplicationDbContext context;

        public CustomerProfileRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(CustomerProfileModel entity)
        {
            context.CustomerProfiles.Add(entity);
            context.SaveChanges();
        }

        public void AddRange(IEnumerable<CustomerProfileModel> entites)
        {
            context.CustomerProfiles.AddRange(entites);
        }

        public void Edit(CustomerProfileModel entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Remove(CustomerProfileModel entity)
        {
            context.CustomerProfiles.Remove(entity);
            context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<CustomerProfileModel> entites)
        {
            context.CustomerProfiles.RemoveRange(entites);
        }

        public void RemoveById(int id)
        {
            context.CustomerProfiles.Remove(FindById(id));
        }

        public IEnumerable<CustomerProfileModel> GetAll()
        {
            return context.CustomerProfiles.ToList();
        }

        public CustomerProfileModel FindById(int id)
        {
            return context.CustomerProfiles.Find(id);
        }

        public CustomerProfileModel FindByUserId(string uid)
        {
            return context.CustomerProfiles.SingleOrDefault(x => x.UserId == uid);
        }

        public CustomerProfileModel FindSingle(Expression<Func<CustomerProfileModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerProfileModel> FindAny(Expression<Func<CustomerProfileModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}