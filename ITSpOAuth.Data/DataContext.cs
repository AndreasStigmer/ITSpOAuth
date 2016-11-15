using ITSpOauth.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSpOAuth.Data
{
    /// <summary>
    /// DataContextklass för EF som används av ITSpOauth.api
    /// </summary>
    public class DataContext:DbContext
    {
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

    }
}
