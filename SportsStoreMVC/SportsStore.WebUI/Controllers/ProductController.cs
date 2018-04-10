using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 3;

        //Constructor
        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            //We have not specified a view name so the default view will be used.
            //Take data in chunks of 4 rows (pagination)
            //return View(repository.Products.OrderBy(p => p.ProductID).Skip((page-1) * PageSize).Take(PageSize));

            //Create a ViewModel object which is initialised with the first page of Products in a specific category, and 
            //the PagingInfo object, set to page 1.
            ProductsListViewModel model = new ProductsListViewModel()
            {
                //Create a products list, which only contains the first page of the currently selected category.
                Products = repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? 
                        repository.Products.Count() :
                        repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category

            };

            //Call the List view, passing it the ViewModel object which is a ProductsListViewModel.
            return View(model);


        }

        //New method to support file upload
        public FileContentResult GetImage(int productId)
        {
            Product prod = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if(prod != null)
            {
                //The File method belongs to the controller base class.
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }

        }


    }
}