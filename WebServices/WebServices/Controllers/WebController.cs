using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//This must be removed or you get an ambiguous reference when you add the [HttpPut] attribute.
//using System.Web.Mvc;

using System.Web.Http;
//Use our own Models folder
using WebServices.Models;




//THIS IS A WEB API CONTROLLER
//You can call it by running the project, then adding /api/web onto the end of the url.

//It has its own routing, nothing to do with the MVC RouteConfig.
//Routing is in App_Start/WebApiConfig
//There is no Action segment in the url. Just the controller name, plus any parameters.
//It decides which action method to call based on the incoming Http verb, which is
//mapped to the name of the Action. 
//When a GET request comes in, it will find action methods with GET anywhere in their names.
//When there are multiple methods with GET in the name, it uses the parameter to narrow down the options.

//This means you get nasty names like PutReservation for Update.
//It is better to use an HTTP attribute, and a nice name.
//PutReservation has been renamed to UpdateReservation and given the HttpPut attribute.

//It will return all the reservation in a format specified by the browser's Accept header.
//IE 11 will ask to download a JSON file. If you click Open in the download request, it will open the JSON in VS.
//Chrome will ask for and display XML.
//(Note when you close Chrome it does not stop the debugger).

namespace WebServices.Controllers
{
    public class WebController : ApiController
    {
        private ReservationRepository repo = ReservationRepository.Current;

        public IEnumerable<Reservation> GetAllReservations()
        {
            return repo.GetAll();
        }
      
        public Reservation GetReservation(int id)
        {
            return repo.Get(id);
        }

        //Better to use an Http attribute
        //public Reservation PostReservation(Reservation item)
        //{
        //    return repo.Add(item);
        //}

        [HttpPost]
        public Reservation CreateReservation(Reservation item)
        {
            return repo.Add(item);
        }

        //Better to use an Http attribute.
        //public bool PutReservation(Reservation item)
        //{
        //    return repo.Update(item);
        //}

        [HttpPut]
        public bool UpdateReservation(Reservation item)
        {
            return repo.Update(item);
        }

        public void DeleteReservation (int id)
        {
            repo.Remove(id);
        }
    }
}