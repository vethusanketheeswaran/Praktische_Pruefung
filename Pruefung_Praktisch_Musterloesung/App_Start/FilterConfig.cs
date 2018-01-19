using System.Web;
using System.Web.Mvc;

namespace Pruefung_Praktisch_Musterloesung
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
