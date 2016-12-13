using System.Web;
using System.Web.Mvc;

namespace TheGeekStore
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
#if !DEBUG
            // Add HTTPS Redirect only on Release.
            GlobalFilters.Filters.Add(new RequireHttpsAttribute());
#endif


        }
    }
}
