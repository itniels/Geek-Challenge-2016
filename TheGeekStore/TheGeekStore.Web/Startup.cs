using System.Collections.Generic;
using Hangfire;
using Hangfire.Common;
using Hangfire.Dashboard;
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

            // Hangfire settings
            BackgroundJobServerOptions opt = new BackgroundJobServerOptions();
            opt.WorkerCount = 20;
            opt.Queues = new[] { "critical", "default" };

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                AuthorizationFilters = new[] { new MyRestrictiveAuthorizationFilter() }
            });
            app.UseHangfireServer(opt);

            // HangFire Jobs
            var manager = new RecurringJobManager();
            // Keep the cart DB cleaned once daily!
            manager.AddOrUpdate("CleanupCarts", Job.FromExpression(() => new HangFire.Jobs().CleanupCarts()), Cron.Daily(), new RecurringJobOptions { QueueName = "critical" });
            // Chnage the daily deals with 3 new random deals!
            // FYI this is set to 5 minutes for demo purposes
            manager.AddOrUpdate("DailyDeals", Job.FromExpression(() => new HangFire.Jobs().DailyDeals(3)), Cron.MinuteInterval(5), new RecurringJobOptions { QueueName = "default" });

        }
    }
}

/// <summary>
/// Authorization filter for HangFire
/// </summary>
public class MyRestrictiveAuthorizationFilter : IAuthorizationFilter
{
    public bool Authorize(IDictionary<string, object> owinEnvironment)
    {
        var context = new OwinContext(owinEnvironment);

        // Allow Admin users to see the Dashboard ONLY.
        return context.Authentication.User.IsInRole("Admin");
    }
}