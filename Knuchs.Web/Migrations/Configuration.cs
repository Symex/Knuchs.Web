namespace Knuchs.Web.Migrations
{
    using Knuchs.Web.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Knuchs.Web.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Knuchs.Web.Models.DataContext context)
        {
            var usr = new User()
            {
                Email = "simon@x-pression.ch",
                Id = 1,
                IsAdmin = true,
                Password = "123",
                Username = "Syme"
            };

            context.Users.AddOrUpdate(usr);
        }
    }
}
