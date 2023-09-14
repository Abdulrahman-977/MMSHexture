using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Core.Helper
{
    public class ResetSession : ActionFilterAttribute, IActionFilter
    {
        private readonly SessionHandler _sessionHandler;
        public ResetSession(SessionHandler sessionHandler)
        {
            _sessionHandler = sessionHandler;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var Url = filterContext.HttpContext.Request.Headers["Referer"].ToString().Split('/');
            string LastController = Url.Length >= 2 ? Url.ElementAt(Url.Length - 2) : "";
            string CurrentController = filterContext.RouteData.Values["Controller"].ToString();
            if (CurrentController != LastController)
            {
                _sessionHandler.RemoveKey($"{LastController}Search");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
