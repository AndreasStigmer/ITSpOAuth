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
    public class StsCallbackController : Controller
    {
        /// <summary>
        /// Denna Controller används för att auth servern skall kunna posta 
        /// authcoden efter det att användaren authorized förfrågan från klienten.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            //Authorization koden kommer från AuthServern i QueryString variabeln
            var code= Request.QueryString["code"];
            
            //Skapar en Token Client som skall ansluta till Token Endpoint för att byta auth coden mot en access_token
            TokenClient tc = new TokenClient(Constants.UserProfileSTSTokenEndpoint, "mvcauthcode", "hemligt");
            
            //Token response Skickar authcoden i utbyte mot en Access token
            TokenResponse tr = await tc.RequestAuthorizationCodeAsync(code, Constants.MVCAPPSTSCallback);

            //Sätter en cookie i användarens webläsare med access token för att inte begära ny access token i varje anrop
            Response.Cookies["UserProfileCookie"]["accessToken"] = tr.AccessToken;

            //Redirectar användaren till den resurs han/hon försökte komma åt när auth flowet började
            return Redirect(Request.QueryString["state"]);
        }
    }
}