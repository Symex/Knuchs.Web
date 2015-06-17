using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Knuchs.Web.Models;
using Microsoft.Ajax.Utilities;

namespace Knuchs.Web.Controllers
{
    public class HomeController : Controller
    {
        public static DataContext Db = new DataContext();

        public ActionResult Index()
        {
            var model = Db.BlogEntries.Where(m => m.Id > 0).ToList();          

            return View("Blog", model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Ihre App-Beschreibungsseite.";

            return View();
        }

        public ActionResult ShowComments(int entryId)
        {
            var comments = Db.BlogEntries.Where(m => m.Id == entryId);
            return View("Comments", comments);
        }
    }
}
