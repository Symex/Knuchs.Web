using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knuchs.Web.Models
{
    public class EditMyProfileViewModel
    {
        public User User { get; set; }
        public string OldPW { get; set; }
        public string NewPW { get; set; }
        public string NewPW2 { get; set; }

    }
}