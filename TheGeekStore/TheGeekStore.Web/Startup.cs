using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheGeekStore.Startup))]
namespace TheGeekStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Default
            ConfigureAuth(app);
            
            // SignalR
            app.MapSignalR();
            
            // HangFire
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            // HangFire Jobs
            // TODO
        }
    }
}
