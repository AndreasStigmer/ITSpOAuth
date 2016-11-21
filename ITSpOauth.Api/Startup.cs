using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Collections.Generic;
using ITSpOAuth.Shared;
using System.Web.Http;

[assembly: OwinStartup(typeof(ITSpOauth.Api.Startup))]

namespace ITSpOauth.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var options = new IdentityServer3.AccessTokenValidation.IdentityServerBearerTokenAuthenticationOptions();
            options.RequiredScopes = new List<string> { "studentScope" };
            options.IssuerName = Constants.IssuerURI;
            options.Authority = Constants.UserProfileSTS;

            app.UseIdentityServerBearerTokenAuthentication(options);

            HttpConfiguration conf = new HttpConfiguration();
            WebApiConfig.Register(conf);
            app.UseWebApi(conf);
           
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
