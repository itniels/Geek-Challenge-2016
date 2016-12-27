using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TheGeekStore.Core.Models;
using TheGeekStore.Core.ViewModels;
using TheGeekStore.Models;
using TheGeekStore.Repositories;

namespace TheGeekStore.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private ApplicationDbContext context;
        private CustomerProfileRepository profiles;
        private PurchaseRepository purchases;

        public CustomerController()
        {
            context = new ApplicationDbContext();
            profiles = new CustomerProfileRepository(context);
            purchases = new PurchaseRepository(context);
        }

        // GET: Customer
        [HttpGet]
        public ActionResult MyPage()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult GetPartialProfile()
        {
            string uid = User.Identity.GetUserId();
            ApplicationUser user = context.Users.SingleOrDefault(x => x.Id == uid);
            CustomerProfileViewModel model = new CustomerProfileViewModel();
            model.Username = user.UserName;

            Session["customerpage"] = "Profile";
            return PartialView("_CustomerProfile", model);
        }

        [HttpPost]
        public ActionResult GetPartialProfile(CustomerProfileViewModel model)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            if (ModelState.IsValid)
            {
                var user = manager.FindById(User.Identity.GetUserId());
                user.UserName = model.Username;
                user.Email = model.Username;

                if (!string.IsNullOrEmpty(model.Password1) && !string.IsNullOrEmpty(model.Password2))
                {
                    if (model.Password1 == model.Password2)
                    {
                        manager.ChangePassword(user.Id, model.PasswordCurrent, model.Password1);
                    }
                }
                store.Context.SaveChanges();
                
            }
            return RedirectToAction("MyPage");
        }

        [HttpGet]
        public PartialViewResult GetPartialAddress()
        {
            Session["customerpage"] = "Address";
            CustomerProfileModel model = profiles.FindByUserId(User.Identity.GetUserId());
            if (model == null)
            {
                model = new CustomerProfileModel();
                model.UserId = User.Identity.GetUserId();
                profiles.Add(model);
            }
            return PartialView("_CustomerAddress", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetPartialAddress(CustomerProfileModel model)
        {
            if (ModelState.IsValid)
            {
                profiles.Edit(model);
            }
            return RedirectToAction("MyPage");
        }

        [HttpGet]
        public PartialViewResult GetPartialOrders()
        {
            Session["customerpage"] = "Orders";
            string uid = User.Identity.GetUserId();
            ICollection<PurchaseModel> model = purchases.FindByUserId(uid).ToList();
            return PartialView("_CustomerOrders", model);
        }

        public PartialViewResult Details(int id)
        {
            PurchaseModel model = purchases.FindById(id);
            return PartialView("_OrderDetails", model);
        }
    }
}