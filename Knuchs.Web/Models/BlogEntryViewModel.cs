using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knuchs.Web.Models
{
    public class BlogEntryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Text { get; set; }
        public int CommentsCount { get; set; }

    }
}