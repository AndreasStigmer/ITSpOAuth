using ITSpOauth.Models.Factory;
using ITSpOAuth.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITSpOauth.Models.ViewModels;
using ITSpOauth.Models;

namespace ITSpOauth.Api.Controllers
{
    /// <summary>
    /// Hantera olika användarprofiler
    /// </summary>
    public class ProfileController : ApiController
    {
        /// <summary>
        /// Hämtar en lista av UserProfileViewModels
        /// Eftersom EF inte känner till ModelFactory tvingas datan ut från databasen
        /// med "db.UserProfiles.ToList()" innan projektionen sker med Select. 
        /// </summary>
        /// <returns>Json serialiserad lista med UserProfileViewModels</returns>
        [HttpGet]
        [Authorize]
        public IHttpActionResult Get()
        {
            ModelFactory mf = new ModelFactory();
            DataContext db = new DataContext();
            var profiles = db.UserProfiles.ToList().Select(d => mf.Create(d)).ToList<UserProfileViewModel>();
            return Json(profiles);
        }

        [HttpPost]
        public IHttpActionResult Post(UserProfileViewModel p)
        {
            ModelFactory mf = new ModelFactory();
            DataContext db = new DataContext();
            var profiles = db.UserProfiles.ToList().Select(d => mf.Create(d)).ToList<UserProfileViewModel>();
            return Json(profiles);
        }
    }
}
