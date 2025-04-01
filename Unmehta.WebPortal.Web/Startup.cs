using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Unmehta.WebPortal.Web.Startup))]
namespace Unmehta.WebPortal.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
