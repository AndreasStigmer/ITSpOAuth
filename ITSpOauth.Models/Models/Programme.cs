using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ITSpOauth.Models
{
    public class Programme
    {
        [Key]
        public int ProgrammeID { get; set; }

        public string Name { get; set; }

        public virtual List<UserProfile> Students { get; set; }
    }
}
