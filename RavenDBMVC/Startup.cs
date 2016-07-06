using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RavenDBMVC.Startup))]
namespace RavenDBMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
