using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ch5EssentialTools.Models
{
    public class LinqValueCalculator :IValueCalculator
    {
        private IDiscountHelper discounter;

        //Constructor
        public LinqValueCalculator(IDiscountHelper discountParam)
        {
            discounter = discountParam;
        }

        public decimal ValueProducts(IEnumerable<Product> products)
        {
            //return products.Sum(p => p.Price);

            //Use the injected discounter to deduct 10%
            return discounter.ApplyDiscount(products.Sum(p => p.Price));
        }
    }
}