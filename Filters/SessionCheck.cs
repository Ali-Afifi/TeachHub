using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace online_course_platform.Filters;

public class SessionCheck : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var httpContext = filterContext.HttpContext;

        if (string.IsNullOrEmpty(httpContext.Session.GetString("_Token")))
        {
            filterContext.Result = new RedirectToRouteResult(
                                            new RouteValueDictionary
                                            {
                                                { "Controller", "Login" },
                                                { "Action", "" }
                                            });

        }
    }

}
