using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HM.WebApp.Startup))]
namespace HM.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
