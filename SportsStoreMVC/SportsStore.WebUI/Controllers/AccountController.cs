using System.Web.Mvc;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{

    public class AccountController : Controller
    {
        IAuthProvider authProvider;

        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }

        public ViewResult Login()
        {
            //User will be redirected to here when they first try to access Admin/Index.
            //This shows the Login view, which has a form which posts back to the Login overload below.
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {

            //The login view has a form which posts back to here.
            //The incoming returnUrl is Admin/Index because that is where we were heading
            //before we go redirected to the login view.
            //This incoming viewmodel's properties map to the html elements on the form, ie the username and password.
            //These two incoming params are courtesy of Model Binding, so we don't know 
            //where it gets the returnUrl from. It returns the LoginviewModel because that
            //is the type define at the top of the Login view. But how does it know the returnUrl?

            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}