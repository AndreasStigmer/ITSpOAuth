using IdentityModel.Client;
using ITSpOAuth.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;


namespace ITSpOauth.MvcClient.Client
{
    public class HttpCLientHelper
    {
        public static HttpClient GetClient()
        {
            HttpClient hc = new HttpClient();
            hc.DefaultRequestHeaders.Clear();
            hc.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.
                MediaTypeWithQualityHeaderValue("application/json"));

            hc.BaseAddress = new Uri(Constants.UserProfileAPI);
            var token = GetAuthCode();
            hc.SetBearerToken(token);

            return hc;

        }


        public static string GetAuthCode()
        {
            var cookie = HttpContext.Current.Request.Cookies["cookie"];
            if (cookie != null && cookie["token"] != null)
            {
                return cookie["token"];
            }

            AuthorizeRequest ar = new AuthorizeRequest(Constants.UserProfileSTSAuthorizeEndpoint);
            var state = HttpContext.Current.Request.Url.OriginalString;
            var url = ar.CreateAuthorizeUrl("mvcauthcode", "code", "studentScope", Constants.MvcAuthCodeCallback,state);
            HttpContext.Current.Response.Redirect(url);

            return null;

        }
        public static string GetToken()
        {
            var cookie = HttpContext.Current.Request.Cookies["cookie"];
            if (cookie != null && cookie["token"] != null) {
                return cookie["token"];
            }

            TokenClient tc = new TokenClient(
                Constants.UserProfileSTSTokenEndpoint,
                "mvcclientcredentials",
                "hemligt");

            TokenResponse tr = tc.RequestClientCredentialsAsync("studentScope").Result;
            var token = tr.AccessToken;
            if (token != null) {
                HttpContext.Current.Response.Cookies["cookie"]["token"] = token;
            }

            return token;
        }
    }
}