using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FootballGameApplication.Startup))]
namespace FootballGameApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
