using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using IdentityModel.Client;
using System.Security.Claims;
using System.Diagnostics;
using System.IdentityModel.Tokens;
using System.Collections.Generic;
using IdentityModel;

[assembly: OwinStartup(typeof(ItSpOauth.MvcOpenIdConnect.Startup))]

namespace ItSpOauth.MvcOpenIdConnect
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap =
                new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "cookie"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions() {

                SignInAsAuthenticationType = "cookie",
                Authority = ITSpOAuth.Shared.Constants.UserProfileSTS,
                RedirectUri= ITSpOAuth.Shared.Constants.MvcOpenIdCallback,
                ClientId ="mvcopenid",
                ClientSecret="hemligt",
                //Begär Auth Code för att begära access token samt en Id token
                ResponseType ="code id_token token",
                //Användaren skall authoriza klienten till dessa scopes
                Scope="openid profile email studentScope roles",

                //NOtifications för att kunna modifiera processningen 
                //under authorization flow
                Notifications=new OpenIdConnectAuthenticationNotifications()
                {
                    //När alla tokens är validerade anropas SecurityTokenValidated
                    //funcen. I denna skapas en Ny ren ClaimsIdentity utan
                    //onödiga claims. Ett unikt name claim skapas och
                    //access token lagras som claim för att användas vid ev api förfrågningar
                    SecurityTokenValidated= async n=>
                    {
                        //Hämtar alla Claims tillhörande identityn från UserInfoEndpoint
                        UserInfoClient ui = new UserInfoClient(ITSpOAuth.Shared.Constants.UserProfileSTSUserInfoEndpoint);
                        var response = await ui.GetAsync(n.ProtocolMessage.AccessToken);
                        //Skapa ny tom claim och anger AuthenticationType samt
                        //namnet på Name och Role claims i identityn
                        ClaimsIdentity ci = 
                        new ClaimsIdentity(n.AuthenticationTicket.Identity.AuthenticationType,
                            "name","role");//n.AuthenticationTicket.Identity as ClaimsIdentity;
                        //Lägger till alla claims från UserInfo till vår ClaimsIdentity
                        ci.AddClaims(response.Claims);

                        //Generera NameClaim
                        var iss = n.AuthenticationTicket.Identity.FindFirst(JwtClaimTypes.Issuer).Value;
                        var sub = n.AuthenticationTicket.Identity.FindFirst(JwtClaimTypes.Subject).Value;

                        Claim name = new Claim("name", iss + sub);
                        ci.AddClaim(name);


                        Claim tokenClaim = new Claim("access_token", n.ProtocolMessage.AccessToken);
                        ci.AddClaim(tokenClaim);


                        //Bygger upp en ny AuthenticationTicket baserat på nya Identityn
                        //samt befintliga properties
                        n.AuthenticationTicket = new Microsoft.Owin.Security
                            .AuthenticationTicket(ci, n.AuthenticationTicket.Properties);
                        
                    }

                }
                
            });
            
        }
    }
}
