using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Packages required
// install-package jquery -version 1.10.2
// install-package bootstrap -version 3.0.0
// install-package knockoutjs -version 3.0.0

namespace WebServices.Models
{
    public class ReservationRepository
    {
        //Singleton
        private static ReservationRepository repo = new ReservationRepository();

        public static ReservationRepository Current
        {
            get { return repo; }
        }

        private List<Reservation> data = new List<Reservation>()
        {
            new Reservation {ReservationID =1, ClientName="Adam", Location="Board Room"},
            new Reservation {ReservationID =2, ClientName= "John", Location="Lecture Hall"},
            new Reservation {ReservationID =3, ClientName="Russel", Location="Meeting Room 1"}
        };

        public IEnumerable<Reservation> GetAll()
        {
            return data;
        }

        public Reservation Get(int id)
        {
            return data.Where(r => r.ReservationID == id).FirstOrDefault();
        }

        public Reservation Add(Reservation item)
        {
            item.ReservationID = data.Count + 1;
            data.Add(item);
            return item;
        }

        public Reservation Remove(int id)
        {
            Reservation item = Get(id);
            if(item != null)
            {
                  data.Remove(item);
            }
          
            return item;
        }

        public bool Update(Reservation item)
        {
            Reservation storedItem = Get(item.ReservationID);
            if(storedItem != null)
            {
                storedItem.ClientName = item.ClientName;
                storedItem.Location = item.Location;
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}