using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeDecide.Startup))]
namespace WeDecide
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
