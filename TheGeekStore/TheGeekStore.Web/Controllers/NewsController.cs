using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGeekStore.Core.Models;
using TheGeekStore.Repositories;

namespace TheGeekStore.Controllers
{
    public class NewsController : Controller
    {
        private NewsRepository news = new NewsRepository();
        // GET: News
        public ActionResult Index()
        {
            return View(news.GetAll());
        }

        public ActionResult ReadArticle(int id)
        {
            return View(news.FindById(id));
        }
    }
}