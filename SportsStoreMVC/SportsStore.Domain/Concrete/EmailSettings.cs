using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "";
        public string MailFromAddress = "";
        public bool UseSsl = true;
        public string Username = "";
        public string Passwrod = "";
        public string Servername = "";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = "";
       
    }
}
