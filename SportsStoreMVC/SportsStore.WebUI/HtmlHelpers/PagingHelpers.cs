using System;
using System.Text;
using System.Web.Mvc;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        //This is an extension method.
        //The "this" param means that it extends the HtmlHelper class.
        //Views can see this helper object using @Html.
        
        //The Func param will accept a delegate which has a single int param, which is the page number.
        //The delegate must return a string, which is going to be an Html anchor tag.
        //The method we send into this param is a llamda expression: x => Url.Action("List", new {page = x })
        //which is a helper which generates a url to the List method and passes a page number.

        //It will be the List method on the ProductsController which gets passed in this delegate.
        //The result is that clicking on one of the generated links will call the List method
        //on the controller, and redisplay the same view, but with a different page of data.

        //Note, this still uses the querystring to pass the page number as part of the url.
        //Http://localhost/?page=2
        //We are going to change it to use a "Composable Url":
        //Http://localhost/Page2
        //This is done by adding a new route to the RouteConfig.cs in the App_Start folder.

        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, 
            Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for(int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                //Build an Html anchor tag
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if(i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }//if
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());


        }

    
    }
}