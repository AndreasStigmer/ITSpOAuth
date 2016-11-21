using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using ITSpOAuth.Shared;
using IdentityServer3.Core.Configuration;
using System.Security.Cryptography.X509Certificates;

[assembly: OwinStartup(typeof(ITSpOAuth.OAuth.Startup))]

namespace ITSpOAuth.OAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Mappar IdentityServer till pathen /identity
         
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

                //kopplar in middlewaren i OWIN pipen idsrv är IAppBuildern
                idsrv.UseIdentityServer(options);
            });
            
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
