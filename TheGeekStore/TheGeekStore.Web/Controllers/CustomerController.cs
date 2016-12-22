using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheGeekStore.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult MyPage()
        {
            return View();
        }

        public ActionResult GetUserPage()
        {
            return View();
        }

        public ActionResult GetAddressPage()
        {
            return View();
        }

        public ActionResult GetOrdersPage()
        {
            return View();
        }
    }
}