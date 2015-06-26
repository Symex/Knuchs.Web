using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Knuchs.Web.Helper;
using Knuchs.Web.Models;
using Microsoft.Ajax.Utilities;

namespace Knuchs.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Check Cookie and Set complete Session if there is one.
            List<BlogEntry> model = new List<BlogEntry>();
            try
            {
                using (var dc = new DataContext())
                {
                    model = dc.BlogEntries.Where(m => m.Id > 0).ToList();
                    model.OrderByDescending(m => m.CreatedOn);
                    return View("Blog", model);
                }
            }
            catch (Exception ex)
            {

                model.Add(new BlogEntry() { Text = ex.Message });

                return View("Blog", model);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Ihre App-Beschreibungsseite.";

            return View();
        }

        public ActionResult ShowComments(int entryId)
        {
            using (var dc = new DataContext())
            {
                ViewBag.EntryId = entryId;
                var entry = dc.BlogEntries.First(m => m.Id == entryId);
                ViewBag.Heading = "Discussion for Topic: " + entry.Title;
                var comments = dc.Comments.Where(m => m.RefBlogEntry.Id == entryId).OrderByDescending(m=> m.CreatedOn).ToList<Comment>();
                var vmCmts = new List<CommentViewModel>();
                foreach (var cmt in comments)
                {
                    vmCmts.Add(ViewModelParser.GetViewModelFromComment(cmt));
                }


                return View("Discussion", vmCmts);
            }
        }

    }
}
