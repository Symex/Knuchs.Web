using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Knuchs.Web.Models
{
    public class Links
    {
        [Key]
        public int Id { get; set; }
        public string LinkText { get; set; }
        public string LinkUrl { get; set; }

    }
}