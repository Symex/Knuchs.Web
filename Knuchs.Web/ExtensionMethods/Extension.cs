using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Knuchs.Web.Models;

namespace Knuchs.Web.Helper
{
    public static class Extension
    {
        public static KnuchsSession GetSession<T>(this T ctx) where T : HttpContextBase
        {
            if (ctx.Session != null) return (KnuchsSession)ctx.Session["KnuchsSession"];
            return null;
        }
    }
}