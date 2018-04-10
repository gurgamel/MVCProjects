using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;


namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

       
        public void SaveProduct(Product product)
        {
            //Add or Update

            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                //The incoming product is not one that came from Ef, so EF does
                //not know about it. So we need to get a product that EF does
                //know about so we can update it.
                Product dbEntry = context.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;

                    //Added to support file uploads
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

       
        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Products.Find(productID);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            //Not sure why we return the thing we just deleted. Probably just convention.
            return dbEntry;
        }
    }
}
