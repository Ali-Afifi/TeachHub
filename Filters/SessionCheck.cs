using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace online_course_platform.Filters;

public class SessionCheck : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var httpContext = filterContext.HttpContext;

        var token = httpContext.Session.GetString("_Token");

        if (string.IsNullOrEmpty(token))
        {
            filterContext.Result = new RedirectToRouteResult(
                                            new RouteValueDictionary
                                            {
                                                { "Controller", "Login" },
                                                { "Action", "" }
                                            });

        }
        else
        {
            System.Console.WriteLine($"token: {token}");

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            var userId = jwtSecurityToken.Claims.First(claim => claim.Type == "Id").Value;
            var userName = jwtSecurityToken.Claims.First(claim => claim.Type == "UserName").Value;
            var userRole = jwtSecurityToken.Claims.First(claim => claim.Type == "Role").Value;

            System.Console.WriteLine($"token_user_id: {userId}");
            System.Console.WriteLine($"token_username: {userName}");
            System.Console.WriteLine($"token_role: {userRole}");


            httpContext.Items["userId"] = userId; 
            httpContext.Items["userName"] = userName; 
            httpContext.Items["userRole"] = userRole; 


        }
    }

}
