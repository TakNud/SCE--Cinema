using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SCECinema.Startup))]
namespace SCECinema
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
