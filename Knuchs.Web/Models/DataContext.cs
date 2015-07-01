using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Knuchs.Web.Models
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<BlogEntry> BlogEntries { get; set; }
        public DbSet<Links> Links { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
    }

}
