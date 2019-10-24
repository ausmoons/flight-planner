using System.Web.Mvc;

namespace flight_planner
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) //ja ir pašu taisīts filts, tad šeit tie tiek ielikti
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
