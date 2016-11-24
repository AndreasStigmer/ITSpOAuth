using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using IdentityServer3.AccessTokenValidation;
using ITSpOAuth.Shared;
using System.Web.Http;

[assembly: OwinStartup(typeof(ITSpOauth.Api.Startup))]

namespace ITSpOauth.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(
                new IdentityServerBearerTokenAuthenticationOptions() {
                    Authority=Constants.UserProfileSTS,
                    IssuerName=Constants.IssuerURI,
                    RequiredScopes=new[] {"studentScope"},
                   

            });

            HttpConfiguration conf = new HttpConfiguration();
            WebApiConfig.Register(conf);
            app.UseWebApi(conf);
        
        }
    }
}
