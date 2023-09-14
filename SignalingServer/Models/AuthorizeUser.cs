using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication5.Models
{
    public class AuthorizeUser : ActionFilterAttribute, IActionFilter
    {
        public string roles { get; set; }
        public AuthorizeUser(string _role)
        {
            roles = _role;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var role = filterContext.HttpContext.Session["role"];
            if (filterContext.HttpContext.Session[UserToken.USER_SESSION_VALUE] == null || UserToken.Token == null || string.IsNullOrEmpty(UserToken.Token.authData.tokenInfo.token))
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                  {"Controller","Identity"},
                  {"Action","Login"}
                });
            }
            else if (filterContext.HttpContext.Session[UserToken.USER_SESSION_VALUE] == null || !roles.Split(',').ToList().Contains(filterContext.HttpContext.Session["role"]))
            {
                filterContext.HttpContext.Session.Clear();
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                  {"Controller","Identity"},
                  {"Action","Login"}
                });
            }
            
                base.OnActionExecuting(filterContext);
        }
    }
}