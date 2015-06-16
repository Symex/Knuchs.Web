using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Knuchs.Web.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool HasNewsletter { get; set; }
        public string PictureLink { get; set; }
        public bool IsAdmin{ get; set; }
    }
}