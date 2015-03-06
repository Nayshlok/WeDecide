using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using WeDecide.Models.Concrete;
using WeDecide.Hubs;
using WeDecide.DAL.Concrete;
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
            GlobalHost.DependencyResolver.Register(typeof(NotificationHub), () => new NotificationHub(new CustomMembershipDAL(new QuestionDbContext())));
            app.MapSignalR();
        }
    }
}
