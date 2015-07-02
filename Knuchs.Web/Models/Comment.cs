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
        public virtual BlogEntry RefBlogEntry { get; set; }
        public virtual User RefUser { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}