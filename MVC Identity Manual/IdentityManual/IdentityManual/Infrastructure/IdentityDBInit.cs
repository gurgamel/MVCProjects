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
    public class IdentityDBInit : DropCreateDatabaseIfModelChanges<AppIdentityDbContext>
    {
        protected override void Seed(AppIdentityDbContext context)
        {
            //This will create the database using code-first
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(AppIdentityDbContext context)
        {
            //Code which creates the database goes here
        }

    }
}