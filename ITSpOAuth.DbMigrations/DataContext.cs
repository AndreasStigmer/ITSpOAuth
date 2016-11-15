using ITSpOauth.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSpOAuth.DbMigrations
{
    public class DataContext:DbContext


    {
        public DataContext() : base("ProgrammesDB")
        {
            
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Programme> Programmes { get; set; }
    }
}
