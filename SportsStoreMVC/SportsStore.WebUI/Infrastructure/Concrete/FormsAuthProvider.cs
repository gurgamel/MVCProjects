using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Security;
using SportsStore.WebUI.Infrastructure.Abstract;

//We are using a little concreate wrapper around the static methods
//in FormsAuthentication, because static methods cannot be mocked.

namespace SportsStore.WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            //This comes from Freemans MVC5 book, and is now obsolete.
            //It is better to use Memebership, such as Membeship.ValidateUser.
            bool result = FormsAuthentication.Authenticate(username, password);

            if(result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }
    }
}