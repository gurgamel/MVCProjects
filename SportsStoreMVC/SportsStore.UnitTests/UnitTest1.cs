using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Linq;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;

using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;


namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //Mock the repository with more than 4 rows
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}

            });

            //Create ProductController
            ProductController controller = new ProductController(mock.Object);

            //Set the page size to 3 rows
            controller.PageSize = 3;
            //get page 1, which is P1, P2, P3
            IEnumerable<Product> result = (IEnumerable<Product>)controller.List(1).Model;

            Product[] prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 3);
            Assert.AreEqual(prodArray[0].Name, "P1");
            Assert.AreEqual(prodArray[1].Name, "P2");
            Assert.AreEqual(prodArray[2].Name, "P3");

            //get page 2, which is P4, P5
            result = (IEnumerable<Product>)controller.List(2).Model;

            prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
            

        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //Define an html helper, required so we can apply the PageLinks extension method to it.
            HtmlHelper myHelper = null;

            //Create the ViewModel object to be passed to the view
            PagingInfo pagingInfo = new PagingInfo { CurrentPage = 2, TotalItems = 28, ItemsPerPage = 10 };
            
            //build the delgate param, which takes i and appends it to the string "Page"
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //Call the PageLinks extension method which should generate some html
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>" + 
                @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>" + 
                @"<a class=""btn btn-default"" href=""Page3"">3</a>", result.ToString());




        }
    }
}
