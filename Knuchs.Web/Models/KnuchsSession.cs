using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knuchs.Web.Models
{
    public class KnuchsSession
    {
        public User CurrentUser { get; set; }
        public List<BlogEntryViewModel> CurrentFavourites { get; set; }

        public KnuchsSession()
        {
            this.CurrentFavourites = new List<BlogEntryViewModel>();

        }
    }


}