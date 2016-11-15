namespace ITSpOAuth.Data.Migrations
{
    using ITSpOauth.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ITSpOAuth.Data.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ITSpOAuth.Data.DataContext context)
        {
            context.Programmes.AddOrUpdate(p =>p.Name, new Programme { Name = "ITSpåret" });
            context.UserProfiles.AddOrUpdate(p => p.FirstName, new UserProfile { FirstName = "Andreas", LastName = "Stigmer" });
         }
    }
}
