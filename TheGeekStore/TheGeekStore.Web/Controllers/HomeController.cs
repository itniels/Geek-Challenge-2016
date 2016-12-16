using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGeekStore.Core.ViewModels;
using TheGeekStore.Models;
using TheGeekStore.Repositories;

namespace TheGeekStore.Controllers
{
    public class HomeController : Controller
    {
        private ProductRepository products = new ProductRepository();
        private NewsRepository news = new NewsRepository();

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
    }
}