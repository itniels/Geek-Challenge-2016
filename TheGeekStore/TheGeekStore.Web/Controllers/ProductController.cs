using System;
using System.Collections.Generic;
using System.IO;
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
        private ApplicationDbContext context;
        private ProductRepository products;
        private CategoryRepository categories;
        private CartRepository carts;

        public ProductController()
        {
            context = new ApplicationDbContext();
            products = new ProductRepository(context);
            categories = new CategoryRepository(context);
            carts = new CartRepository(context);
        }

        public ActionResult ViewProduct(int id)
        {
            ViewProductViewModel model = new ViewProductViewModel();
            model.Product = products.FindById(id);
            model.PeopleReadyToBuy = carts.ProductCountInCarts(model.Product);
            model.RelatedProducts = model.Product.Category.Products.Where(x => x.Id != id).Take(3);
            return View(model);
        }

        // ==============================================================================
        // CREATE
        // ==============================================================================

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public PartialViewResult Create()
        {
            ViewBag.Categories = categories.GetAll();
            return PartialView("_CreateProduct");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("Index", "Admin");
            }

            // Set data
            ProductModel item = new ProductModel
            {
                Name = model.Name,
                Description = model.Description,
                Category = categories.FindByName(model.CategoryName),
                Featured = model.Featured,
                TimesPuchased = 0,
                ProductNumber = model.ProductNumber.Length == 0 ? GenerateProductNumber(model.CategoryName) : model.ProductNumber,
            InStock = model.InStock,
                Price = model.Price
            };

            // uploaded file
            if (model.UploadedFile != null && model.UploadedFile.ContentLength > 0)
            {
                string directory = Server.MapPath("~/Content/Images/ProductImages/");
                string filename = model.Name + Path.GetExtension(model.UploadedFile.FileName);
                var path = Path.Combine(directory, filename);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                model.UploadedFile.SaveAs(path);
                item.ImagePath = "/Content/Images/ProductImages/" + filename;
            }
            else
            {
                // Add default NO IMAGE image.
                item.ImagePath = "/Content/Images/ProductImages/" + "no_product_image.png";
            }

            // Save to database
            products.Add(item);

            return RedirectToAction("Index", "Admin");
        }

        // ==============================================================================
        // EDIT
        // ==============================================================================
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult Edit(int id)
        {
            ViewBag.Categories = categories.GetAll();

            ProductModel product = products.FindById(id);
            if (product == null)
                return null;

            EditProductViewModel model = new EditProductViewModel();
            model.Id = product.Id;
            model.Name = product.Name;
            model.Description = product.Description;
            model.CategoryName = product.Category.Name;
            model.ProductNumber = product.ProductNumber;
            model.Price = product.Price;
            model.InStock = product.InStock;
            model.Featured = product.Featured;

            return PartialView("_EditProduct", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Edit(EditProductViewModel model)
        {
            ProductModel product = products.FindById(model.Id);
            product.Name = model.Name;
            product.Description = model.Description;
            product.Category = categories.FindByName(model.CategoryName);
            product.ProductNumber = model.ProductNumber.Length == 0 ? GenerateProductNumber(model.CategoryName) : model.ProductNumber;
            product.Price = model.Price;
            product.InStock = model.InStock;
            product.Featured = model.Featured;

            // uploaded file
            if (model.UploadedFile != null && model.UploadedFile.ContentLength > 0)
            {
                // Remove old
                if (!product.ImagePath.Contains("no_product_image.png"))
                    System.IO.File.Delete(product.ImagePath);

                // Add new
                string directory = Server.MapPath("~/Content/Images/ProductImages/");
                string filename = model.Name + Path.GetExtension(model.UploadedFile.FileName);
                var path = Path.Combine(directory, filename);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                model.UploadedFile.SaveAs(path);
                product.ImagePath = "/Content/Images/ProductImages/" + filename;
            }

            products.Edit(product);
            return RedirectToAction("Index", "Admin");
        }


        // ==============================================================================
        // DELETE
        // ==============================================================================
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult DeleteConfirmation(int id)
        {
            ProductModel model = products.FindById(id);
            return PartialView("_ProductDelete", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Delete(int id)
        {
            ProductModel model = products.FindById(id);
            products.Remove(model);
            return RedirectToAction("index", "Admin");
        }

        private string GenerateProductNumber(string categoryName)
        {
            var n1 = categoryName[0].ToString().ToUpper();
            var n2 = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day;
            var n3 = DateTime.Now.TimeOfDay.TotalSeconds.ToString();

            return n1 + n2 + n3;
        }
    }


}