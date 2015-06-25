using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using DotNetOpenAuth.AspNet;
using Knuchs.Web.Helper;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Knuchs.Web.Filters;
using Knuchs.Web.Models;
using Newtonsoft.Json;
using System.Data.Entity;

namespace Knuchs.Web.Controllers
{
    public class AccountController : Controller
    {

        #region AnonymousActions
    
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            //if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null)
            //    returnUrl = Server.UrlEncode(Request.UrlReferrer.PathAndQuery);

            //if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            //{
            //    ViewBag.ReturnURL = returnUrl;
            //}
            return View("Login", new LoginModel() { ErroMessage = "", HasError = false, RememberMe = true, ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginCallBack(LoginModel u)
        {
            if (IsValidUser(u))
            {
                using (var _db = new DataContext())
                {
                    HttpContext.GetSession().CurrentUser = _db.Users.First(m => m.Password == u.Password && u.Username == m.Username);
                    if (u.RememberMe)
                    {
                            var json = JsonConvert.SerializeObject(HttpContext.GetSession().CurrentUser);
                            var userCookie = new HttpCookie("RememberTheKnuchs", json);
                            userCookie.Expires.AddDays(30);
                            HttpContext.Response.SetCookie(userCookie);

                            return RedirectToLocal(u.ReturnUrl);
                    }
                }
            }

            return RedirectToLocal(u.ReturnUrl);

        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            //Directly Log User in.
            return View();
        }

        #endregion

        #region AdminActions

        [AuthorizeAdmin]
        public ActionResult ManageUsers()
        {
            var lsUser = new List<User>();
            using (var _db = new DataContext())
            {
                lsUser = _db.Users.ToList<User>();
            }
            return View("ManageUsers", lsUser);
        }

        [AuthorizeAdmin]
        public ActionResult WriteEntry()
        {
            return View("EntryEditor", new BlogEntry());
        }

        [AuthorizeAdmin]
        public ActionResult ManageBlogEntries()
        {
            var lsEntries = new List<BlogEntry>();
            using (var dc = new DataContext())
            {
                lsEntries = dc.BlogEntries.ToList();
            }
            return View("ManageBlogEntries", lsEntries);
        }

        [AuthorizeAdmin]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EntryCallback(BlogEntry be)
        {
            using (var dc = new DataContext())
            {
                if (be.Id != 0)
                {
                    var e = dc.BlogEntries.First(m => m.Id == be.Id);
                    e.Text = be.Text;
                    //e.CreatedOn = DateTime.Now;
                    e.Title = be.Title;
                    dc.SaveChanges();

                }
                else
                {

                    be.CreatedOn = DateTime.Now;
                    dc.BlogEntries.Add(be);
                    dc.SaveChanges();
                }
            }

            return RedirectToAction("ManageBlogEntries", "Account");
        }

        [AuthorizeAdmin]
        public ActionResult EditBlogEntry(int entryId)
        {
            var blogEntry = new BlogEntry();
            using (var dc = new DataContext())
            {
                blogEntry = dc.BlogEntries.First(m => m.Id == entryId);
            }

            return View("EntryEditor", blogEntry);

        }

        [AuthorizeAdmin]
        public ActionResult MakeUserAdmin(int UserId)
        {
            using (var db = new DataContext())
            {
                var user = db.Users.First(u => u.Id == UserId);
                user.IsAdmin = true;
                db.SaveChanges();
            }

            return RedirectToAction("ManageUsers", "Account");
        }

        [AuthorizeAdmin]
        public ActionResult DeleteUser(int UserId)
        {
            using (var db = new DataContext())
            {
                var user = db.Users.First(u => u.Id == UserId);
                db.Users.Remove(user);
                db.SaveChanges();
            }

            return RedirectToAction("ManageUsers", "Account");
        }

        [AuthorizeAdmin]
        public ActionResult DeleteBlogEntry(int entryId)
        {
            using (var db = new DataContext())
            {
                var entry = db.BlogEntries.First(e => e.Id == entryId);
                db.BlogEntries.Remove(entry);
                db.SaveChanges();
            }

            return RedirectToAction("ManageBlogEntries", "Account");
        }

        [AuthorizeAdmin]
        public ActionResult CreateUser()
        {
            var u = new User() { IsAdmin = false };

            return View("CreateUser", u);
        }

        [AuthorizeAdmin]
        public ActionResult DeleteComment(int id)
        {
            using (var db = new DataContext())
            {
                var cmt = db.Comments.First(m => m.Id == id);
                var eId = cmt.RefBlogEntry.Id;
                db.Comments.Remove(cmt);
                db.SaveChanges();

                return RedirectToAction("ShowComments", "Home", new { @entryId = eId });//eId });
            }          
        }

        [AuthorizeAdmin]
        public ActionResult CreateUserCallBack(User u)
        {
            using (var db = new DataContext())
            {
                u.HasNewsletter = false;

                db.Users.Add(u);
                db.SaveChanges();
                ViewBag.Message = "Der User wurde erfolgreich erstellt.";
            }

            return RedirectToAction("ManageUsers", "Account");
        }


        #endregion

        #region UserActions

        [AuthorizeUser]
        public ActionResult NewComment(string NewCommentText, int EntryId, string NewCommentTitle)
        {
            using(var dc = new DataContext())
            {
                var entry = dc.BlogEntries.First(m=>m.Id == EntryId);
                var aidee = HttpContext.GetSession().CurrentUser.Id;

                var user = dc.Users.First(m => m.Id == aidee);
                var cmt = new Comment(){
              
                CreatedOn = DateTime.Now,
                Text = NewCommentText,
                Title = NewCommentTitle,
                };

                dc.Comments.Add(cmt);
                dc.SaveChanges();

                //Set References, just to make sure.
                dc.Comments.Attach(cmt);
                cmt.RefUser = user;
                cmt.RefBlogEntry = entry;
                dc.Entry(cmt).State = EntityState.Modified;
                dc.Entry(cmt.RefBlogEntry).State = EntityState.Modified;
                dc.Entry(cmt.RefUser).State = EntityState.Modified;


                dc.SaveChanges();
            }
            //Return Discussion for given Entry ID
            return RedirectToAction("ShowComments", "Home" , new { entryId = EntryId });
        }

        [AuthorizeUser]
        public ActionResult LogOff()
        {
            //Destroy Cookie if there is one 
            HttpContext.GetSession().CurrentUser = null;
            if (Request.Cookies["RememberTheKnuchs"] != null)
            {
                var c = new HttpCookie("RememberTheKnuchs");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Hilfsprogramme

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private bool IsValidUser(LoginModel u)
        {
            using (var _db = new DataContext())
            {
                var usr = _db.Users.First(m => m.Username == u.Username && m.Password == u.Password);
                if (usr != null)
                {
                    HttpContext.GetSession().CurrentUser = usr;
                    return true;
                }
                HttpContext.GetSession().CurrentUser = null;
                return false;
            }
        }

        #endregion
    }
}
