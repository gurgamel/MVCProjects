using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ch5EssentialTools.Models;
//using Ninject; Not required now we have the dependency resolver registered

namespace Ch5EssentialTools.Controllers
{
    public class HomeController : Controller
    {
        //Gets 'calc' from the ninject dependency resolver in the infrastructure folder.
        //The DR was registered in the NinjectWebCommon.cs in the App_Start folder.
        private IValueCalculator calc;

        private Product[] products = 
        {
            new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
            new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
            new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.5M},
            new Product {Name = "Corner Flag", Category = "Soccer", Price = 34.95M}
        };

        //Constructor. This uses constructor injection DI
        public HomeController(IValueCalculator calcParam)
        {
            calc = calcParam;
        }

        // GET: Home
        public ActionResult Index()
        {
            //Ninject stuff. Superceded by the dependency resolver in the Infrastrucure folder.
            //IKernel ninjectKernel = new StandardKernel();
            //ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
            //IValueCalculator calc = ninjectKernel.Get<IValueCalculator>();
            
            //replaces this
            //LinqValueCalculator calc = new LinqValueCalculator();

            ShoppingCart cart = new ShoppingCart(calc) { Products = products};
            decimal totalValue = cart.CalculateProductTotal();

            return View(totalValue);
        }
    }
}