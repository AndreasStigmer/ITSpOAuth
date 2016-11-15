using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using IdentityServer3.Core.Configuration;
using ITSpOAuth.OAuth;
using System.Security.Cryptography.X509Certificates;
using ITSpOAuth.Shared;
using System.Diagnostics;
using System.Web.Configuration;

[assembly: OwinStartup(typeof(ITSpOAuth.AuthServer.Startup))]

namespace ITSpOAuth.AuthServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/identity", idsrv =>
            {
                //Sökväg till certifikat och privat nyckel
                var certfile = AppDomain.CurrentDomain.BaseDirectory + @"\Certificates\OAuthSign.pfx";

                //Factory som konfigurerar typen av användare, clients och scopes
                var factory = new IdentityServerServiceFactory()
                    .UseInMemoryUsers(InMemory.GetUsers())
                    .UseInMemoryScopes(InMemory.GetScopes())
                    .UseInMemoryClients(InMemory.GetClients());

                //Optionsobjekt som används för att konfigurera identityserver middleware
                var options = new IdentityServerOptions();
                options.Factory = factory;
                options.SigningCertificate = new X509Certificate2(certfile, "password");
                options.IssuerUri = Constants.IssuerURI;
                options.PublicOrigin = Constants.UserProfileSTSOrigin;
                options.SiteName = "Programme site";
                
                //kopplar in middlewaren i OWIN pipen
                idsrv.UseIdentityServer(options);
                Debug.Write(WebConfigurationManager.GetSection("system.webServer").ToString());
                Debug.Write("Andreas");
            });

            
           
        }
    }
}
