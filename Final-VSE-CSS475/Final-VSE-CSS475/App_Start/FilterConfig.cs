using System.Web;
using System.Web.Mvc;

namespace Final_VSE_CSS475
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
