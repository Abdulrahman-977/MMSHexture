using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Core.Helper
{
    public class AuthorizeUser : ActionFilterAttribute, IActionFilter
    {
        private readonly UserToken _token;
        public AuthorizeUser(UserToken token)
        {
            _token = token;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_token.Token == null || _token.Token.authData == null)
            {
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
