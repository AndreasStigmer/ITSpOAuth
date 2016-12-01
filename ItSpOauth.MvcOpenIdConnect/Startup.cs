using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using IdentityModel.Client;
using System.Security.Claims;
using System.Diagnostics;

[assembly: OwinStartup(typeof(ItSpOauth.MvcOpenIdConnect.Startup))]

namespace ItSpOauth.MvcOpenIdConnect
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
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
                ResponseType="code id_token token",
                Scope="openid profile email studentScope",
                Notifications=new OpenIdConnectAuthenticationNotifications()
                {
                    SecurityTokenValidated= async n=>
                    {
                        UserInfoClient ui = new UserInfoClient(ITSpOAuth.Shared.Constants.UserProfileSTSUserInfoEndpoint);
                        var response = await ui.GetAsync(n.ProtocolMessage.AccessToken);
                        ClaimsIdentity ci = n.AuthenticationTicket.Identity as ClaimsIdentity;
                        ci.AddClaims(response.Claims);

                        Claim tokenClaim = new Claim("access_token", n.ProtocolMessage.AccessToken);
                        ci.AddClaim(tokenClaim);

                        foreach(Claim c in response.Claims)
                        {
                            Debug.WriteLine(c.Type + ":"+c.Value);
                        }


                    }

                }
                
            });
            
        }
    }
}
