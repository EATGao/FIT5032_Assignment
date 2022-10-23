using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HealingTalkNearestYou.CustomSecurity
{
    public class CustomAuthorization : FilterAttribute, IAuthorizationFilter
    {
        public string UserType { get; set; }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!filterContext.HttpContext.User.IsInRole(UserType))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
                }
            }
            else 
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
        
        }
    }
}