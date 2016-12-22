using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGeekStore.Core.Models;
using TheGeekStore.Models;
using TheGeekStore.Web.Repositories;

namespace TheGeekStore.Controllers
{
    public class MenuController : Controller
    {
        private CategoryRepository categories;

        public MenuController()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            categories = new CategoryRepository(context);
        }

        // GET: Menu
        public IEnumerable<CategoryModel> GetCategories()
        {
            return categories.GetAll();
        }
    }
}