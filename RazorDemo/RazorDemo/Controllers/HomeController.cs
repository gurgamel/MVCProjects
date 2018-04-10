using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RazorDemo.Models;

namespace RazorDemo.Controllers
{
    public class HomeController : Controller
    {
        Product _myProduct = new Product()
        {
            ProductID = 1,
            Name = "Kayak",
            Description = " a boat",
            Category = "Watersports",
            Price = 275M
        };



        // GET: Home
        public ActionResult Index()
        {
            return View(_myProduct);
        }

        public ActionResult NameAndPrice()
        {
            return View(_myProduct);
        }

    }
}