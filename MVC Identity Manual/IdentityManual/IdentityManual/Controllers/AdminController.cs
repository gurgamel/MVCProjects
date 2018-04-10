using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using IdentityManual.Infrastructure;

using IdentityManual.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace IdentityManual.Controllers
{
    public class AdminController : Controller
    {
        //This property is just to simplify the Index method below
        private AppUserManager UserManager
        {
            //OWIN has added an extension methods to the HttpContext class.
            //This provides a per-request OWIN context.
            //GetOwinContext an GetUserManager<T> are both extension methods.
            //AppUserManager is one of our own classes.
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }
                
        public ActionResult Index()
        {
            //This enumerates the users from the IdentityDb database.
            //It call the Index in the Admin folder, view passing the list of users as a model.
            //UserManager.Users returns list of AppUser objects (from the Models folder).
            return View(UserManager.Users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Name, Email = model.Email };

                //Create the new user asynchronously
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    //Call the Index method on this controller again so we refresh the list of users
                    return RedirectToAction("Index");
                }
                else
                {
                    //Put any errors from the Result into the ModelState. The view can then display them.
                    AddErrorsFromResult(result);
                }
            }
            //We will only get to here if the creation of the user fails.
            //If it succeeds we will have return further up. Nasty.
            //Redisplay the view along with any errors we have added to the ModelState.
            return View(model);
        }
        
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            //Get the user to be deleted
            AppUser user = await UserManager.FindByIdAsync(id);

            if(user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if(result.Succeeded)
                {
                    //Redisplay the list of users
                    return RedirectToAction("Index");
                }
                else
                {
                    //Show the error view
                    return View("Error", result.Errors);
                }
            }
            else
            {
                //Show the error view
                return View("Error", new string[] { "User not found" });
            }
        }
        
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach(string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public async Task<ActionResult> Edit(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);

            if(user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, string email, string password)
        {
            //The arguments arrive via model binding. They must have the same names the fields on the Create view.

            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                //Is the new user valid?
                user.Email = email;
                IdentityResult validEmail = await UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;

                //Do we have a pw?
                if (password != string.Empty)
                {
                    //Is the pw valid
                    validPass = await UserManager.PasswordValidator.ValidateAsync(password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash =
                        UserManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                //Is the email valid?
                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded
                && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(user);
        } 
       



    }//class
}//ns































