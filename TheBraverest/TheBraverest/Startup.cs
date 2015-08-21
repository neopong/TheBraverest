using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheBraverest.Startup))]
namespace TheBraverest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
