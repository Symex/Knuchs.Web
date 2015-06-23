using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Knuchs.Web.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public BlogEntry RefBlogEntry { get; set; }
        public User RefUser { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Title { get; set; }
    }
}