using IdentityModel.Client;
using ITSpOAuth.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ITSpOauth.MvcClient.Controllers
{
    public class CallbackController : Controller
    {
        // GET: Callback
        public async Task<ActionResult> Index()
        {
            var code = Request.QueryString["code"];

            TokenClient tc = new TokenClient(
               Constants.UserProfileSTSTokenEndpoint,
               "mvcauthcode",
               "hemligt");

            TokenResponse tr = await tc.RequestAuthorizationCodeAsync(code, Constants.MvcAuthCodeCallback);
            var token = tr.AccessToken;
            if (token != null)
            {
                Response.Cookies["cookie"]["token"] = token;
            
            }

            Response.Redirect(Request.QueryString["state"]);


            return View();
        }
    }
}