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
            var blogEntry = new BlogEntry()
            {
                CreatedOn = DateTime.Now,
                Id = 1,
                Text = "Some Usefull Text",
                Title = "This is the Title"
            };
            ViewBag.Message = "Ändern Sie diese Vorlage als Schnelleinstieg in Ihre ASP.NET MVC-Anwendung.";
            

            return View("Blog",blogEntry);
        }
        public ActionResult Blog()
        {
            ViewBag.Message = "Ändern Sie diese Vorlage als Schnelleinstieg in Ihre ASP.NET MVC-Anwendung.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Ihre App-Beschreibungsseite.";

            return View();
        }
    }
}
