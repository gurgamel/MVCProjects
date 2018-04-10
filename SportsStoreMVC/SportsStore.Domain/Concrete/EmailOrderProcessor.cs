
using System.Text;
using System.Net.Mail;
using System.Net;

using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;

namespace SportsStore.Domain.Concrete
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        //Constructor
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using(var smptClient = new SmtpClient())
            {
                //Load cart into email and send it

            }//using
        }


    }
}
