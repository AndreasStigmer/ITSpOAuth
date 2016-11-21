using IdentityModel.Client;
using ITSpOAuth.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace ITSpOauth.MvcClient.Helpers
{
    public class HttpClientHelper
    {
        public static HttpClient GetHttpClient()
        {
           // HttpContext.Current.Response.Cookies.Clear();
            HttpClient c = new HttpClient();
            c.BaseAddress = new Uri(Constants.UserProfileAPI);
            
            //Hämtar en AccessToken
            // c.SetBearerToken(GetAccesToken());
            var token = RequestAaccessTokenAuthCode();
            if(token!=null)
            {
                c.SetBearerToken(token);
            }
            //Sättter en accept header
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return c;

        }

        /// <summary>
        /// Denna metod begär en auth code ifrån Auth servern. Innan den begär koden
        /// kollar den UserProfileCookie om det redan finns en access_token. Isåfall används denna
        /// istället.
        /// </summary>
        /// <returns></returns>
        private static string RequestAaccessTokenAuthCode() {

            //kontrollerar om access token redan finns hos användaren och returnerar den isåfall
            HttpCookie cookie = HttpContext.Current.Request.Cookies["UserProfileCookie"];
            if (cookie != null && cookie["accessToken"] != null)
            {
                return cookie["accessToken"];
            }


            //En Authorizr request som skall skicka data till Authorize endpoint för att få en auth code
            AuthorizeRequest authorizeRequest = new AuthorizeRequest(Constants.UserProfileSTSAuthorizationEndpoint);
            //PLockar ut den urlen som användaren efterfrågade när authize flowet påbörjades
            string returnToUrl = HttpContext.Current.Request.Url.OriginalString;
            /*
             * Bygger upp en url med querystring för att skicka till auth endpointen
            Består av ClientId vad vi efterfrågar="code" samt vilket scope vi vill få tillgång till, vilken
            url som AuthServern skall posta authcoden till samt den url som användaren skall skickas till när
            authensieringen är klar
            
             */
            string url = authorizeRequest.CreateAuthorizeUrl("mvcauthcode", "code", "studentScope", Constants.MVCAPPSTSCallback,returnToUrl);

            //Redirectar webläsaren till auth servern authorize endpoint där en användare får logga in
            //samt authorize åtkomsten till scopet
            HttpContext.Current.Response.Redirect(url);
            return null;

        }

        //TokenClient ingår i identitymodel nuget paketet
        public static string GetAccesToken() {

            var cookie = HttpContext.Current.Request.Cookies["UserProfileCookie"];
            if (cookie != null && cookie["accessToken"] != null) {
                return cookie["accessToken"];
            }

            TokenClient tc = new TokenClient(Constants.UserProfileSTSTokenEndpoint,
                "mvcclient",
                "hemligt"
                );

            //Hämtar en token som innehåller scopet studentScope
            var tokenResponse = tc.RequestClientCredentialsAsync("studentScope").Result;
            //Lagrar access_token i en cookie
            HttpContext.Current.Response.Cookies["UserProfileCookie"]["accessToken"]=tokenResponse.AccessToken;

            return tokenResponse.AccessToken;
        }
    }
}