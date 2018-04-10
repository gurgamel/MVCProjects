using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ch5EssentialTools.Models
{
    public interface IDiscountHelper
    {
        decimal ApplyDiscount(decimal totalParam);
        
    }

    public class DiscountHelper : IDiscountHelper
    {
        public decimal ApplyDiscount(decimal totalParam)
        {
            //deduct 10% from totalParam
            return (totalParam - (10m / 100m * totalParam));
        }
    }
}