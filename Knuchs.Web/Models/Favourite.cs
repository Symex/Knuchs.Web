using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Knuchs.Web.Models
{
    public class Favourite
    {
       public int Id { get; set; }

       public BlogEntry RefFavBlog { get; set; }

       public User RefFavUser { get; set; }
    }
}