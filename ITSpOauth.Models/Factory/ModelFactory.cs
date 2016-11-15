using ITSpOauth.Models;
using ITSpOauth.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSpOauth.Models.Factory
{

    /// <summary>
    /// Den här klassen används för att konvertera modelobjekt till Viewmodelobjek
    /// som kan serialiseras.
    /// </summary>
    public  class ModelFactory
    {
        /// <summary>
        /// Tar emot en UserProfile och returnerar motsvarande ViewModelobjekt
        /// </summary>
        /// <param name="p">UserProfile som skall konverteras till motsvarande ViewModel</param>
        /// <returns>UserProfileViewModel</returns>
        public  UserProfileViewModel Create(UserProfile p) {
            return new UserProfileViewModel {
                UserProfileID = p.UserProfileID,
                FirstName = p.FirstName,
                LastName = p.LastName,
                ProgrammeIDs = p.Programmes.Select(d => d.ProgrammeID).ToList<int>()
            };
        }

        /// <summary>
        /// Tar emot en Programme och returnerar en ProgrammeviewModel
        /// </summary>
        /// <param name="p">Programme objekt som skall göras om till ViewModel</param>
        /// <returns>ProgrammeViewModel</returns>
        public ProgrammeViewModel Create(Programme p)
        {
            return new ProgrammeViewModel
            {
                Name = p.Name,
                ProgrammeID = p.ProgrammeID,
                StudentIDs=p.Students.Select(d=>d.UserProfileID).ToList<int>()
            };
        }
    }
}
