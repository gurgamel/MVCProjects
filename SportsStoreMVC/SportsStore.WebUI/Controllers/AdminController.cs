using System.Linq;
using System.Web.Mvc;
using System.Web; //added to support saving images


using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    //This is using an MVC feature called Filters for authorisation.
    //Without params the Authorise attribute allow access to all authenticated users.
    //We can apply this to methods, or to the whole class.
    
    //On first attempt to access Admin/Index, the user has not been authenticated.
    //They are redirected to the Login view as defined in the web.config (authentication section).
    //The form on the login view posts back to the Login method on the Account controller.
    //One of the params to the Login method is the url to the Index method here.
    //When the authentication succeeeds, the Login method redirects to the returnUrl which sends the user
    //back to the Index method here.

    //Don't know how the url for Admin/Index gets into the returnUrl param.
    //The Login view was not given it explicitly, so how does it pass it back?.

    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        //Constructor
        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        // GET: Admin
        public ViewResult Index()
        {
            return View(repository.Products);
        }

        
        public ViewResult Edit(int productId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        //Overload for handling form postbacks
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            //Returned actionResult derives from ViewResult.
            //It does call a view. The RedirectToAction returns an ActionResult although we don't
            //do anything with it.

            //The incoming product is populated by Model Binding.
            //Its property names must map the html elements in the Edit view.
            //The ProductId property comes from a hidden field on the edit view.

            //Because the view can now upload an image, the image is sent here
            //as a separate param, not part of the product. More model binding and invisible plumbing.

            if (ModelState.IsValid)
            {
                //If we have an image, put it into the Product for saving.
                if(image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }

                //Save the product
                repository.SaveProduct(product);
                TempData["message"] = product.Name + " has been saved";

                //Redirect to the Index method to show the list of products again
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }

        public ViewResult Create()
        {
            //Call the Edit view with an empty product
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted",
                    deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

    }
}