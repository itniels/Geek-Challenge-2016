using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGeekStore.Core.Models;
using TheGeekStore.Web.Repositories;

namespace TheGeekStore.Controllers
{
    public class MenuController : Controller
    {
        private CategoryRepository categories = new CategoryRepository();
        // GET: Menu
        public IEnumerable<CategoryModel> GetCategories()
        {
            return categories.GetAll();
        }
    }
}