using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ITSpOauth.Models
{
    public class UserProfile
    {
        [Key]
        public int UserProfileID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual List<Programme> Programmes { get; set;}
    }
}
