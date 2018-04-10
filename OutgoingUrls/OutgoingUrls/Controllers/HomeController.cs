using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutgoingUrls.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "Index";

            //If a custom segment variable such as "id" has been used in the RouteConfig, we can access it
            //ViewBag.SomeValue = RouteData.Values["id"];
            ViewBag.SomeValue = "empty";

            return View("ActionName");
        }

        //A method which shows how we can pass a param using 
        //a custom segment var in the routeconfig.
        public ActionResult CustomAction(string id = "")
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "CustomAction";

            //The incoming id is provided by the route in RouteConfig.
            //This is all done with binding, no code.
            //It only works because the param has the same name as custom seg var in the route.
            //Or the view called this action method passing an anon type which has property called "id".
            if(!string.IsNullOrEmpty(id))
            {
                ViewBag.SomeValue = "Incoming ID is " + id;
            }
            else
            {
                //Blank the viewbag value otherwise it persists from the last call.
                ViewBag.SomeValue = "empty, we did not get an incoming id";
            }
           

            return View("ActionName");
        }
    }
}