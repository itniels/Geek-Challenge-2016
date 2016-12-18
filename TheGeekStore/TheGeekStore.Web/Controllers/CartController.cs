using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGeekStore.Core.JsonModels;
using TheGeekStore.Core.Models;

namespace TheGeekStore.Controllers
{
    public class CartController : Controller
    {
        public ActionResult ViewCart()
        {
            return View();
        }

        public JsonResult GetCartInfo()
        {
            var model = new JsonCartInfo();
            CartModel cart = null;
            if (User.Identity.IsAuthenticated)
            {
                
            }
            return null;
        }
    }
}