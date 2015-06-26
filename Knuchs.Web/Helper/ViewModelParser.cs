using Knuchs.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knuchs.Web.Helper
{
    public static class ViewModelParser
    {
        public static CommentViewModel GetViewModelFromComment(Comment cmt)
        {
            try
            {
                var cvm = new CommentViewModel();
                cvm.BlogEntryId = cmt.RefBlogEntry.Id;
                cvm.Username = cmt.RefUser.Username;
                cvm.CreatedOn = cmt.CreatedOn;
                cvm.Title = cmt.Title;
                cvm.Text = cmt.Text;
                cvm.Id = cmt.Id;
                cvm.IsAdmin = cmt.RefUser.IsAdmin;

                return cvm;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}