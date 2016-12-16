﻿using System;
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
    public class AdminController : Controller
    {
        // Repositories
        private CategoryRepository categories = new CategoryRepository();
        private ProductRepository products = new ProductRepository();

        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public PartialViewResult GetPartialOverview()
        {
            Session["adminpage"] = "/Admin/GetPartialOverview";
            ApplicationDbContext db = new ApplicationDbContext();
            AdminOverviewViewModel model = new AdminOverviewViewModel();
            model.CountCategories = categories.GetAll().Count();
            model.CountProducts = products.GetAll().Count();
            model.CountUsers = db.Users.Count();
            model.CountFeaturedProducts = products.GetFeaturedProducts().Count();
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