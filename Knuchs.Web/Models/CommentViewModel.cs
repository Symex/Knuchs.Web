using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knuchs.Web.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int BlogEntryId { get; set; }
        public string Username { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Title { get; set; }
        public bool IsAdmin { get; set; }
    }
}