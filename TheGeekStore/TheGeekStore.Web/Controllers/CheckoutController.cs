﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheGeekStore.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult CheckOutCart()
        {
            return View();
        }
    }
}