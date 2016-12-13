using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheGeekStore.Core.Models;
using TheGeekStore.Web.Repositories;

namespace TheGeekStore.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryRepository categories = new CategoryRepository();

        public ActionResult Index()
        {
            IEnumerable<CategoryModel> model = categories.GetAll();
            return View(model);
        }


        public ActionResult Browse(int id)
        {
            var model = categories.FindById(id);
            return View(model);
        }
    }
}