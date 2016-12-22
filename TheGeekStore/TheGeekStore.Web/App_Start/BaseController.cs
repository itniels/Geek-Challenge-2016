//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using TheGeekStore.Models;

//namespace TheGeekStore.App_Start
//{
//    public abstract class BaseController : Controller
//    {
//        protected ApplicationDbContext context { get; set; }

//        protected BaseController()
//        {
//            context = new ApplicationDbContext();
//        }

//        protected override void Dispose(bool disposing)
//        {
//            context.Dispose();
//            base.Dispose(disposing);
//        }
//    }
//}