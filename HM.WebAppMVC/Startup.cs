using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HM.WebAppMVC.Startup))]
namespace HM.WebAppMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
