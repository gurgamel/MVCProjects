using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
//Include our own Models folder
using IdentityManual.Models;

namespace IdentityManual.Infrastructure
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        //A constructor which just calls the base class constructor
        //passing the name of the connection string from the web.config.
        public AppIdentityDbContext() : base("IdentityDb")
        {
            //nothing
        }

        //A private static constructor. Gets used when we call the static Create below.
        static AppIdentityDbContext()
        {
            //IdentityDBInit is one of our classes.
            //It has no constructor just 2 methods: Seed and PerformInitialSetup.
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDBInit());

        }

        //A static method which returns a concrete instance of this class.
        //The static constructor above will be used.
        //OWIN will use this method to create new instances.
        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }

    }
}