using ITSpOauth.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ITSpOauth.MvcClient.Controllers
{
    public class ProfilesController : Controller
    {
        // GET: Profiles
        public async Task<ActionResult> Index()
        {
            //Skapar en HTTPClient med accesstoken
            HttpClient hc=Helpers.HttpClientHelper.GetHttpClient();

            //Hämtar userprofiles från APIet
            var profiles = await hc.GetAsync("api/profile/get").ConfigureAwait(false);
            
            if (profiles.IsSuccessStatusCode)
            {
                var resultString = await profiles.Content.ReadAsStringAsync().ConfigureAwait(false);
                List<UserProfileViewModel> data = JsonConvert.DeserializeObject<List<UserProfileViewModel>>(resultString).ToList();
                return View(data);
            }
            
            return View("Error");
        }
    }
}