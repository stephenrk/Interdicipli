using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mbarber.Startup))]
namespace Mbarber
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
