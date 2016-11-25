using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheGeekStore.Startup))]
namespace TheGeekStore
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
