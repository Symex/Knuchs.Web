
        using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Knuchs.Web.Helper;
using WebMatrix.WebData;
using Knuchs.Web.Models;
using Newtonsoft.Json;

namespace Knuchs.Web.Filters
{
    public class AuthorizeAdminAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase ctx)
        {
            if (ctx.GetSession() != null)
            {
                if (ctx.GetSession().CurrentUser != null)
                {
                    return ctx.GetSession().CurrentUser.IsAdmin;
                }
                //Check for Remember Me cookie
                if (ctx.Request.Cookies["RememberTheKnuchs"] != null)
                {
                    var user = JsonConvert.DeserializeObject<User>(ctx.Request.Cookies["RememberTheKnuchs"].Value);
                   
                    if (ctx.GetSession().CurrentUser == null)
                    {
                        ctx.GetSession().CurrentUser = user;
                        return ctx.GetSession().CurrentUser.IsAdmin;
                    }

                }
            }
           
            return false;
        }
    }
}

    
