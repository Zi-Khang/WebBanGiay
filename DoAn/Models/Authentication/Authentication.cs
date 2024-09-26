using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;


namespace DoAn.Models.Authentication
{
    public class Authentication : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session["Username"] == null)
            {
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary 
                {
                    {"Controller", "Account"},
                    {"Action", "Login"}
                });
            }
        }
    }
}