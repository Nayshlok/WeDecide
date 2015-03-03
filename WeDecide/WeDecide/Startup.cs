using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using WeDecide.Models.Concrete;

[assembly: OwinStartupAttribute(typeof(WeDecide.Startup))]
namespace WeDecide
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new MyIdProvider());
            app.MapSignalR();
        }
    }
}
