using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ITSpOauth.MvcClient.Startup))]
namespace ITSpOauth.MvcClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
