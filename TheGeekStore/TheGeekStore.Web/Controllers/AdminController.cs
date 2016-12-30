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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // Repositories
        private ApplicationDbContext context;
        private CategoryRepository categories;
        private ProductRepository products;
        private PurchaseRepository purchases;
        private CustomerProfileRepository profiles;
        private RoleManager<IdentityRole> roleManager;

        public AdminController()
        {
            context = new ApplicationDbContext();
            categories = new CategoryRepository(context);
            products = new ProductRepository(context);
            purchases = new PurchaseRepository(context);
            profiles = new CustomerProfileRepository(context);
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult GetPartialOverview()
        {
            Session["adminpage"] = "Overview";
            AdminOverviewViewModel model = new AdminOverviewViewModel();

            model.CountCategories = categories.GetAll().Count();
            model.CountProducts = products.GetAll().Count();
            model.CountUsers = roleManager.FindByName("Customer").Users.Select(x => x.UserId).Count();
            model.CountAdmins = roleManager.FindByName("Admin").Users.Select(x => x.UserId).Count();
            model.CountFeaturedProducts = products.GetFeaturedProducts().Count();
            model.CountPurchases = purchases.GetAll().Count();

            return PartialView("_AdminOverview", model);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult GetPartialCategories()
        {
            Session["adminpage"] = "Categories";
            return PartialView("_AdminCategories", categories.GetAll());
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult GetPartialProducts()
        {
            Session["adminpage"] = "Products";
            return PartialView("_AdminProducts", products.GetAll());
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult GetPartialOrders()
        {
            Session["adminpage"] = "Orders";
            return PartialView("_AdminOrders", purchases.GetAll().ToList());
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult OrderDetails(int id)
        {
            PurchaseModel model = purchases.FindById(id);
            return PartialView("_OrderDetails", model);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult GetPartialCustomers()
        {
            Session["adminpage"] = "Customers";
            var users = roleManager.FindByName("Customer").Users;
            ICollection<AdminUserViewModel> model = new List<AdminUserViewModel>();
            foreach (var user in users)
            {
                AdminUserViewModel item = new AdminUserViewModel();
                item.User = context.Users.SingleOrDefault(x => x.Id == user.UserId);
                item.Address = profiles.FindByUserId(item.User.Id);
                item.Orders = purchases.FindByUserId(item.User.Id).ToList();
                model.Add(item);
            }
            
            return PartialView("_AdminCustomers", model);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult GetPartialAdmins()
        {
            Session["adminpage"] = "Admins";
            var users = roleManager.FindByName("Admin").Users;
            ICollection<AdminUserViewModel> model = new List<AdminUserViewModel>();
            foreach (var user in users)
            {
                AdminUserViewModel item = new AdminUserViewModel();
                item.User = context.Users.SingleOrDefault(x => x.Id == user.UserId);
                item.Address = profiles.FindByUserId(item.User.Id);
                item.Orders = purchases.FindByUserId(item.User.Id).ToList();
                model.Add(item);
            }
            return PartialView("_AdminAdmins", model);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
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