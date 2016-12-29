using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGeekStore.Core.Models;
using TheGeekStore.Core.ViewModels;
using TheGeekStore.Models;
using TheGeekStore.Repositories;

namespace TheGeekStore.Controllers
{
    public class HomeController : Controller
    {
        private ProductRepository products;
        private NewsRepository news;

        public HomeController()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            products = new ProductRepository(context);
            news = new NewsRepository(context);
        }

        public ActionResult Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.TopSold = products.GetTop3Sold();
            model.LatestNews = news.GetLatest3();
            model.Featuredproduct = products.GetFeaturedProduct();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "About The Geek Shop";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us.";

            return View();
        }

        public ActionResult Search(string search)
        {
            return View(products.SearchFor(search));
        }
    }
}