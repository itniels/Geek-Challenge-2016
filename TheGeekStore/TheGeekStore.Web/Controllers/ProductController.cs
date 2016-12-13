using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGeekStore.Core.Models;
using TheGeekStore.Core.ViewModels;
using TheGeekStore.Repositories;
using TheGeekStore.Web.Repositories;

namespace TheGeekStore.Controllers
{
    public class ProductController : Controller
    {
        private ProductRepository products = new ProductRepository();
        //private CategoryRepository categories = new CategoryRepository();

        public ActionResult ViewProduct(int id)
        {
            ViewProduct model = new ViewProduct();
            model.Product = products.FindById(id);
            model.RelatedProducts = model.Product.Category.Products.Where(x => x.Id != id).Take(3);
            return View(model);
        }
    }
}