using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TheGeekStore.Core.Models;
using TheGeekStore.Core.ViewModels;
using TheGeekStore.Models;
using TheGeekStore.Repositories;
using TheGeekStore.Web.Repositories;

namespace TheGeekStore.Controllers
{
    public class AdminController : Controller
    {
        // Repositories
        private CategoryRepository categories;
        private ProductRepository products;
        private PurchaseRepository purchases;

        public AdminController()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            categories = new CategoryRepository(context);
            products = new ProductRepository(context);
            purchases = new PurchaseRepository(context);
        }

        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public PartialViewResult GetPartialOverview()
        {
            Session["adminpage"] = "Overview";
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            AdminOverviewViewModel model = new AdminOverviewViewModel();

            model.CountCategories = categories.GetAll().Count();
            model.CountProducts = products.GetAll().Count();
            model.CountUsers = roleManager.FindByName("Customer").Users.Select(x => x.UserId).Count();
            model.CountAdmins = roleManager.FindByName("Admin").Users.Select(x => x.UserId).Count();
            model.CountFeaturedProducts = products.GetFeaturedProducts().Count();
            model.CountPurchases = purchases.GetAll().Count();

            return PartialView("_AdminOverview", model);
        }

        [Authorize(Roles = "Admin")]
        public PartialViewResult GetPartialCategories()
        {
            Session["adminpage"] = "Categories";
            return PartialView("_AdminCategories", categories.GetAll());
        }

        [Authorize(Roles = "Admin")]
        public PartialViewResult GetPartialProducts()
        {
            Session["adminpage"] = "Products";
            return PartialView("_AdminProducts", products.GetAll());
        }

        [Authorize(Roles = "Admin")]
        public PartialViewResult GetPartialStatistics()
        {
            Session["adminpage"] = "Statistics";
            return PartialView("_AdminStatistics");
        }

        [Authorize(Roles = "Admin")]
        public PartialViewResult GetPartialCustomers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var model =
            Session["adminpage"] = "Customers";
            return PartialView("_AdminCustomers");
        }

        [Authorize(Roles = "Admin")]
        public PartialViewResult GetPartialAdmins()
        {
            Session["adminpage"] = "Admins";
            return PartialView("_AdminAdmins");
        }

        [Authorize(Roles = "Admin")]
        public bool ChangeFeatured(int id)
        {
            if (id <= 0)
                throw new ArgumentNullException();

            try
            {
                ProductModel model = products.FindById(id);
                model.Featured = !model.Featured;
                products.Edit(model);
                return model.Featured;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}