using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TheGeekStore.Core.Services
{
    public class BrowserStorage
    {
        private static readonly string KeyCartId = "tgs_cartid";

        public static void SetCartId(HttpContext context, int value)
        {
            HttpCookie cookie = new HttpCookie(KeyCartId);
            cookie.Value = value.ToString();
            context.Response.Cookies.Remove(KeyCartId);
            context.Response.SetCookie(cookie);
        }

        public static int GetCartId(HttpContext context)
        {
            try
            {
                HttpCookie cookie = context.Request.Cookies.Get(KeyCartId);
                int id = Convert.ToInt32(cookie.Value);
                return id;
            }
            catch (Exception)
            {
                return -1;
            }
            
        }

        public static void ClearCartId(HttpContext context)
        {
            context.Response.Cookies.Remove(KeyCartId);
        }
    }
}
