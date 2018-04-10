using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using IdentityManual.Infrastructure;

//This is the OWIN "Start" class. It would usually be called Start,
//but Freeman ignores this convention as we are only using OWIN for Identity.
//This class registers the object which OWIN can instantiate.

namespace IdentityManual.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<AppIdentityDbContext>(AppIdentityDbContext.Create);

            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);

            //The LoginPath is where users are redirected to when they initially seek
            //access without authentication. Auth is then stored in cookies.
            //There will be a controller called Account, with an action method called Login.
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });

        }
    }
}