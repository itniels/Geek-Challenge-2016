using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TheGeekStore.Core.Interfaces;
using TheGeekStore.Core.Models;
using TheGeekStore.Models;

namespace TheGeekStore.Web.Repositories
{
    public class CategoryRepository : IRepository<CategoryModel>, ICategoryRepository
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public void Add(CategoryModel entity)
        {
            context.Categories.Add(entity);
            context.SaveChanges();
        }

        public void AddRange(IEnumerable<CategoryModel> entites)
        {
            context.Categories.AddRange(entites);
            context.SaveChanges();
        }

        public void Edit(CategoryModel entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(CategoryModel entity)
        {
            context.Categories.Remove(entity);
            context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<CategoryModel> entites)
        {
            context.Categories.RemoveRange(entites);
            context.SaveChanges();
        }

        public void RemoveById(int id)
        {
            context.Categories.Remove(FindById(id));
            context.SaveChanges();
        }

        public IEnumerable<CategoryModel> GetAll()
        {
            return context.Categories.Include(x => x.Products);
        }

        public CategoryModel FindById(int id)
        {
            CategoryModel model = context.Categories.Include(i => i.Products).First(x => x.Id == id);
            return model;
        }

        public CategoryModel FindSingle(Expression<Func<CategoryModel, bool>> predicate)
        {
            return context.Set<CategoryModel>().Where(predicate).Single();
        }

        public IEnumerable<CategoryModel> FindAny(Expression<Func<CategoryModel, bool>> predicate)
        {
            return context.Set<CategoryModel>().Where(predicate);
        }
    }
}
