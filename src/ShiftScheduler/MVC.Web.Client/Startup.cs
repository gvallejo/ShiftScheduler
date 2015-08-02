using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC.Web.Client.Startup))]
namespace MVC.Web.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
