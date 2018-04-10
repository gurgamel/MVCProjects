using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

//Added for Range Route contraints
using System.Web.Mvc.Routing.Constraints;

namespace UrlsAndRoutes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Original.
            //The default values provided as ano types in the third param can contain reg ex to
            //constrain the routes.
            //eg  { controller = "^H.*" } will only match controller names that start wiht H.
            //eg  { action = "^Index$¦^About$" } will only match actions names of Index or About.

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            //Default segments
            //Third param is an anonymous type.
            //It means the Controller defaults to "Home", Action defaults to "Index" if they are not supplied.
            //All these are ok:
            // UrlsAndRoutes/
            // UrslAndRoutes/Home
            // UrlsAndRoutes/Home/Index
            
            //routes.MapRoute("MyRoute", "{controller}/{action}", new { controller = "Home", action = "Index" });

            //Static url segments, insert a string literal
            
            //routes.MapRoute("", "Public/{action}", new { controller = "Home", action = "Index" });

            //Custom segments.
            //You don't have to stick with {controller} and {action} as segment variables.
            //You can create your own.
            //{id} is a custom segment variable, which we default to 27.
            //Note that the View can access "id" by calling RouteData.Values["id"].
            //Note. If the action has a param with the same name as the cstom seg var,
            //the value of the seg var will be bound to the param. Model Binding, no code, yuk.
            //Note. If you don't like putting explicit defaults in the route because it
            //breaks separation of concerns, you can just use optional params on the action methods.
            
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}", new { controller = "Home", action = "CustomAction", id = "27" });


            //Make the id an optional part of the url.
            //Not including it in the url means it is ignored.
            //The incoming param to CustomAction is null.

            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}",
            //   new { controller = "Home", action = "CustomAction", id = UrlParameter.Optional });

            //Using * as a catchall means we access any number segments in the url.
            //This will catch any url, the first 3 segs go into the first 3 vars, everything
            //else will be contatenatd into the catchall seg var with no leading or trailing slashes.
            
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //  new { controller = "Home", action = "CustomAction", id = UrlParameter.Optional });

            //Using namespaces to fix controller name collions.
            //The Controllers folder has a HomeController, but there is a copy of it in ControllersOther.
            //The route needs to hit the right one.
            //We add a 4th param to MapRoute, which is an array of anon types, initialised with one
            //item which is the namespace we want.
            //You can add multiple namespaces, but you will still get errors if that creates collisions.
            
            routes.MapRoute("MyRoute", "{controller}/{action}",
              new { controller = "Home", action = "Index" },
              new[] { "UrlsAndRoutes.ControllersOther"});

            //Constrain routes for specific HTTP verbs.
            //(Note this is explicitly pointing at the namespace for the HomeController in Controllers, not ControllersOther).
            //The param name "httpMethod" does not matter, can be anything.
            //This has nothing to do with restricting action methods with HTTP attributes.

            routes.MapRoute("MyRoute", "{controller}/{action}",
             new { controller = "Home", action = "Index", httpMethod = new HttpMethodConstraint("GET") },
             new[] { "UrlsAndRoutes.Controllers" });


            //Using a Range Constraint.
            //You need an extra "using" above for this to work.
            //There is a whole bunch of constraint methods which are listed in Freemans MVC5 book at loc 11928.
            //You can define a custom constraint if you really want to.
            routes.MapRoute("MyRoute", "{controller}/{action}",
            new { controller = "Home", action = "Index", 
                httpMethod = new HttpMethodConstraint("GET"),
                id = new RangeRouteConstraint(10,20)},
            new[] { "UrlsAndRoutes.Controllers" });


            //There is also Attribute routing, but we don't like it because it breaks separation of concerns
            //by putting knowledge of the routing into the controllers.

        }
    }
}
