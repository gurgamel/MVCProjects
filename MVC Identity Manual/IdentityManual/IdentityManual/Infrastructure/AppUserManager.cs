using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using IdentityManual.Models;

namespace IdentityManual.Infrastructure
{
    public class AppUserManager : UserManager<AppUser>
    {
        //Constructor
        public AppUserManager(IUserStore<AppUser> store) :base(store)
        {
            //Just calls the base class constructor
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext owinContext)
        {
            //As soon as we perform operations on a user, Identity will call this Create
            //method.
            
            //The incoming owinContext defines a Get method which returns objects that 
            //have been registered in the OWIN start class.

            //To create a UserStore object, we need an instance of AppIdentityDbContext,
            //which comes from the context.Get via incoming OWIN context.
            AppIdentityDbContext db = owinContext.Get<AppIdentityDbContext>();

            //UserStore is the EF implementation of IUserStore, which provides storage specific
            //implementations of the methods defined in the UserManager base class.
            AppUserManager manager = new AppUserManager(new UserStore<AppUser>(db));

            //Create a password validator that requires at least 6 chars, some upper some lower,
            //but does not require digits or punctuation.
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true
            };

            //Create a user validator
            manager.UserValidator = new UserValidator<AppUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            return manager;
        }

    }
}






