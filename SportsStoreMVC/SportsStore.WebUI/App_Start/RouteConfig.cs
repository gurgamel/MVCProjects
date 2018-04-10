using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //New route which provides a "Composable Url", http://localhost/Page2
            //instead of using the querystring http://localhost/?page=2.
            //For this route to supercede the original default one, it precede the 
            //original one in the file.
            //Routes are processed in the order they are listed here.

            //First param is the name of the route
            //Second param is the url of the route
            //Third param is an object which maps to the controller and action method, plus any
            //params that the action method needs. Our List method needs category and page params.
            routes.MapRoute(null, "",
                    new { controller = "Product", action = "List", category = (string)null, page = 1 }
                );

            //In the second param, the {page} is a placeholder for the actual page number.
            routes.MapRoute(null, "Page{page}",
                new { controller = "Product", action = "List", category = (string)null },
                new { page = @"\d+" }
            );


            routes.MapRoute(null, "{category}",
                new { controller = "Product", action = "List" , page = 1}
            );


            routes.MapRoute(null, "{category}/Page{page}",
             new { controller = "Product", action = "List" },
             new { page = @"\d+" }
             );

            routes.MapRoute(null, "{controller}/{action}");



            ////This route replaces the original below. It allows for a Composable URL like http://localhost/Page2
            //routes.MapRoute(
            //    name: null,
            //    url: "Page{page}",
            //    defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            //);

            ////ORiginal route which defaults to the List method on the ProductController
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            //);

        }
    }
}
