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

namespace Knuchs.Web.Controllers
{
    public class AccountController : Controller
    {

        #region AnonymousActions
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            //if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null)
            //    returnUrl = Server.UrlEncode(Request.UrlReferrer.PathAndQuery);

            //if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            //{
            //    ViewBag.ReturnURL = returnUrl;
            //}
            return View("Login", new LoginModel() { ErroMessage = "", HasError = false, RememberMe = false, ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/LogOff

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
                        //SETCookie
                    }
                }
            }

            return RedirectToLocal(u.ReturnUrl);

        }

        //
        // GET: /Account/Register

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
            return View("Manage",lsUser);
        }

        [AuthorizeAdmin]
        public ActionResult WriteEntry()
        {
            

            return View("EntryEditor", new BlogEntry());
        }

        [AuthorizeAdmin]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EntryCallback(BlogEntry be)
        {
            using (var dc = new DataContext())
            {
                be.CreatedOn = DateTime.Now;
                
                dc.BlogEntries.Add(be);
                dc.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
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

            return RedirectToAction("Index", "Home");
        }

        [AuthorizeAdmin]
        public ActionResult DeleteUser(int UserId)
        {
            using (var db = new DataContext())
            {
                var user = db.Users.First(u => u.Id == UserId);
                user = null;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        [AuthorizeAdmin]
        public ActionResult CreateUser()
        {
            var u = new User() { IsAdmin = false };

            return View("CreateUser", u);
        }

        [AuthorizeAdmin]
        public ActionResult CreateUserCallBack(User u)
        {
            using (var db = new DataContext())
            {
                u.HasNewsletter = false;
               
                db.Users.Add(u);
            }

            return RedirectToAction("ManageUsers", "Account");
        }


        #endregion

        #region UserActions


        [AuthorizeUser]
        public ActionResult LogOff()
        {
            //Destroy Cookie if there is one 
            HttpContext.GetSession().CurrentUser = null;

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
