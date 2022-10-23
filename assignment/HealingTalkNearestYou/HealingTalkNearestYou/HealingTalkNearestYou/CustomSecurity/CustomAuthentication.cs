using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace HealingTalkNearestYou.CustomSecurity
{
    public class CustomAuthentication : FilterAttribute, IAuthenticationFilter
    {
        // execute before action
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            // whether authenticated or not
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
        // after action, before generate result
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login"}));
            }
        }
    }
}