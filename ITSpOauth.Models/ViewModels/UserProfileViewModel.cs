using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSpOauth.Models.ViewModels
{
    /// <summary>
    /// En ViewModelklass som representerar en student eller personal i systemet
    /// </summary>
    public class UserProfileViewModel
    {
        public int UserProfileID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        /// <summary>
        /// För att undvika cirkulära referenser vid JSon serializering
        /// används en lista med id på programm som studente går
        /// </summary>
        public List<int> ProgrammeIDs { get; set; }
      
    }
}
