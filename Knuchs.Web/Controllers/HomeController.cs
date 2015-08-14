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
                    var listVm = new List<BlogEntryViewModel>();
                    model = dc.BlogEntries.Where(m => m.Id > 0).ToList();
                    model = model.OrderByDescending(m => m.CreatedOn).ToList();
                    foreach(var b in model)
                    {
                        var cmts = dc.Comments.Where(c => c.RefBlogEntry.Id == b.Id);
                        var vm = ViewModelParser.GetVMFromBlogEntry(b, cmts.Count());
                        listVm.Add(vm);
                    }
                    return View("Blog", listVm);
                }
            }
            catch (Exception ex)
            {
                var vms = new List<BlogEntryViewModel>();

                return View("Blog", vms);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Ihre App-Beschreibungsseite.";

            return View();
        }

        public ActionResult SearchBlog(string SearchTerm)
        { 
            var model  = new List<BlogEntryViewModel>();
            using (var db = new DataContext())
            {
                var entriesBody = db.BlogEntries.Where(b => b.Text.Contains(SearchTerm)).ToList();
                foreach (var b in entriesBody)
                {
                    var cmts = db.Comments.Where(m => m.RefBlogEntry.Id == b.Id).Count();
                    model.Add(ViewModelParser.GetVMFromBlogEntry(b, cmts));
                }
            }
            if (model.Count > 0)
            {
                ViewBag.Searchresult = true;
                return View("Blog", model);
            }
            else 
            {
                ViewBag.Searchresult = false;
               // model.Add(new BlogEntryViewModel(){ CommentsCount = 0, Id = 0, Title = "Suchergebnisse", Text = "Zu deiner Suche wurden keine Blog-Einträge gefunden"});
                return View("Blog", model);
            }
        }
    }
}
