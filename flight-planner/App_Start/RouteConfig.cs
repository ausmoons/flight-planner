﻿using System.Web.Mvc;
using System.Web.Routing;

namespace flight_planner
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes) //mājaslapai
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
