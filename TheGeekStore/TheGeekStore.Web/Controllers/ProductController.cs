using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGeekStore.Core.Models;
using TheGeekStore.Core.ViewModels;
using TheGeekStore.Models;
using TheGeekStore.Repositories;
using TheGeekStore.Web.Repositories;

namespace TheGeekStore.Controllers
{
    public class ProductController : Controller
    {
        private ProductRepository products;

        public ProductController()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            products = new ProductRepository(context);
        }

        public ActionResult ViewProduct(int id)
        {
            ViewProductViewModel model = new ViewProductViewModel();
            model.Product = products.FindById(id);
            model.RelatedProducts = model.Product.Category.Products.Where(x => x.Id != id).Take(3);
            return View(model);
        }
    }
}