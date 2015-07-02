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
                cvm.Text = cmt.Text;
                cvm.Id = cmt.Id;
                cvm.IsAdmin = cmt.RefUser.IsAdmin;
                cvm.UserImgPath = cmt.RefUser.PictureLink;
                cvm.UserId = cmt.RefUser.Id;

                return cvm;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static BlogEntryViewModel GetVMFromBlogEntry(BlogEntry b, int p)
        {
            var bvm = new BlogEntryViewModel();
            bvm.CommentsCount = p;
            bvm.CreatedOn = b.CreatedOn;
            bvm.Id = b.Id;
            bvm.Text = b.Text;
            bvm.Title = b.Title;
            return bvm;

        }
    }
}