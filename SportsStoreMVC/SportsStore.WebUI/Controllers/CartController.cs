using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System.Diagnostics;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        //constructor
        public CartController(IProductRepository repo, IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }

        public ViewResult Index(string returnUrl)
        {
            //The incoming returnUrl is the url to go to if the user clicks "Continue Shopping"
            //in the cart Index view.

            //Call the Cart\Index view, passing a ViewModel called CartIndexViewModel,
            //which holds the cart and the url to go to if the user clicks "continue shopping".

            CartIndexViewModel vm = new CartIndexViewModel();
            vm.Cart = GetCart();
            vm.ReturnUrl = returnUrl;
            Debug.WriteLine(vm.Cart.Lines.Count());
            return View(vm);

            //return View(new CartIndexViewModel
            //{
            //    //Gets the cart from the Session
            //    Cart = GetCart(),
            //    ReturnUrl = returnUrl
            //});
        }

        public RedirectToRouteResult AddToCart(int productId, string returnUrl)
        {
            //The param names match the names 
            //(not the case) of the input elements on the html forms in the ProductSummary view.
            //MVC maps these params to the incoming POST variables as long as they are the same.

            //Get the product for the incoming id
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if(product != null)
            {
                //Add the product to the cart. The cart is held in Session.
                GetCart().AddItem(product, 1);
            }

            //RedirectToAction asks the client browser to request a new url.
            //We are asking the browser to call the Index method on the CartController.
            //Second param is a string array with one item, because that is what RedirectToAction expects.
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            //Get the incoming product from the respository
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if(product != null)
            {
                GetCart().RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
     
        private Cart GetCart()
        {
            //Get the cart from the Session, or create a new one.
            Cart cart = (Cart)Session["Cart"];
            if(cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;

        }

        public PartialViewResult Summary(Cart cart)
        {
            //The incoming cart param arrives via the custom model binder, so there
            //must be a hidden element in a form on the _Layout called "cart".

            //Added to support the cart summary on the product list page.
            //Creates and returns a partial view using the cart model.
           
            //This action method has its own view called Summary.
            //We pass it the incoming cart model.
            Debug.WriteLine(cart.Lines.Count());
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            //Returns the Checkout view, passing a new ShippingDetails model
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            //An empty cart is no good
            if(cart.Lines.Count() == 0)
            {
                //Add an error to the ModelState which can be detected by the view.
                ModelState.AddModelError("", "Sorry, your cart is empty");

            }

            //Are both incoming objects valid?
            //They have data validation attributes will determine their validity.
            //Presumably this will return false if either the Cart or the ShippingDetails is invalid.
            if(ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                //Return the "Completed" view (in the Views/Cart folder)
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

    }
}