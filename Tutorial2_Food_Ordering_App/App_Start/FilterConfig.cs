using System.Web;
using System.Web.Mvc;

namespace Tutorial2_Food_Ordering_App
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
