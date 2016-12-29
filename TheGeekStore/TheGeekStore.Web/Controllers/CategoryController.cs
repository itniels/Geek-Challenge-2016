using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using TheGeekStore.Core.Models;
using TheGeekStore.Core.ViewModels;
using TheGeekStore.Models;
using TheGeekStore.Web.Repositories;

namespace TheGeekStore.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryRepository categories;

        public CategoryController()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            categories = new CategoryRepository(context);
        }

        // ==============================================================================
        // INDEX
        // ==============================================================================

        public ActionResult Index()
        {
            IEnumerable<CategoryModel> model = categories.GetAll();
            return View(model);
        }

        // ==============================================================================
        // BROWSE
        // ==============================================================================

        public ActionResult Browse(int id)
        {
            var model = categories.FindById(id);
            return View(model);
        }

        // ==============================================================================
        // CREATE
        // ==============================================================================

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public PartialViewResult Create()
        {
            return PartialView("_CreateCategory");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Admin");
            }

            // Set data
            CategoryModel category = new CategoryModel
            {
                Name = model.Name,
                Description = model.Description
            };

            // uploaded file
            if (model.UploadedFile != null && model.UploadedFile.ContentLength > 0)
            {
                string directory = Server.MapPath("~/Content/Images/CategoryImages/");
                string filename = model.Name + Path.GetExtension(model.UploadedFile.FileName);
                var path = Path.Combine(directory, filename);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                model.UploadedFile.SaveAs(path);
                category.ImagePath = "/Content/Images/CategoryImages/" + filename;
            }
            else
            {
                // Add default NO IMAGE image.
                category.ImagePath = "/Content/Images/CategoryImages/" + "no_category_image.png";
            }

            // Save to database
            categories.Add(category);

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
            CategoryModel category = categories.FindById(id);
            if (category == null)
                return null;

            EditCategoryViewModel model = new EditCategoryViewModel();
            model.Id = category.Id;
            model.Name = category.Name;
            model.Description = category.Description;

            return PartialView("_EditCategory", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            CategoryModel category = categories.FindById(model.Id);
            category.Name = model.Name;
            category.Description = model.Description;

            // uploaded file
            if (model.UploadedFile != null && model.UploadedFile.ContentLength > 0)
            {
                // Remove old
                if (!category.ImagePath.Contains("no_category_image.png"))
                    System.IO.File.Delete(category.ImagePath);

                // Add new
                string directory = Server.MapPath("~/Content/Images/CategoryImages/");
                string filename = model.Name + Path.GetExtension(model.UploadedFile.FileName);
                var path = Path.Combine(directory, filename);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                model.UploadedFile.SaveAs(path);
                category.ImagePath = "/Content/Images/CategoryImages/" + filename;
            }

            categories.Edit(category);
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
            CategoryModel model = categories.FindById(id);
            return PartialView("_CategoryDelete", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Delete(int id)
        {
            CategoryModel model = categories.FindById(id);
            categories.Remove(model);
            return RedirectToAction("index", "Admin");
        }
    }
}