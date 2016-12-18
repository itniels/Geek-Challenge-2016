using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheGeekStore.Controllers
{
    public class DailyDealsController : Controller
    {
        // GET: DailyDeals
        public ActionResult Index()
        {
            return View();
        }
    }
}