using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSpOauth.Models.ViewModels
{
    /// <summary>
    /// En ViewModelklass som representerar ett utbildningsprogram i systemet
    /// </summary>
    public class ProgrammeViewModel
    {
            public int ProgrammeID { get; set; }
            public string Name { get; set; }

            /// <summary>
            /// För att undvika cirkulära referenser vid JSson serializering
            /// används en lista med id på studenter som går programmet
            /// </summary>
            public List<int> StudentIDs { get; set; }
    }
}
