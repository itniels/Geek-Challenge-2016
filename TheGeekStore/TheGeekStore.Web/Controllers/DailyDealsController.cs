using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGeekStore.Models;
using TheGeekStore.Repositories;

namespace TheGeekStore.Controllers
{
    public class DailyDealsController : Controller
    {
        private ApplicationDbContext context;
        private DailyDealRepository dailyDeals;

        public DailyDealsController()
        {
            context = new ApplicationDbContext();
            dailyDeals = new DailyDealRepository(context);
        }

        public ActionResult Index()
        {
            return View(dailyDeals.GetAll());
        }
    }
}