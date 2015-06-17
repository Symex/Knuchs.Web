﻿
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
            }
            return false;
        }
    }
}

    