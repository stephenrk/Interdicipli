using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NorthwindSeguranca.Startup))]
namespace NorthwindSeguranca
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
