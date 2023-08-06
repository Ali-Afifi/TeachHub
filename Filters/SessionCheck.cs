using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace online_course_platform.Filters;

public class SessionCheck : ActionFilterAttribute
{
    private static List<string> GetStudentAllowedRoutes(int id)
    {
        var routes = new List<string>();

        routes.Add("/");
        routes.Add("/Home");
        routes.Add("/Home/Privacy");
        routes.Add("/Home/NotFoundError");
        routes.Add($"/Students/Details/{id}");
        routes.Add("/Logout");

        return routes;
    }

    private static List<string> GetInstructorAllowedRoutes(int id)
    {
        var routes = new List<string>();

        routes.Add("/");
        routes.Add("/Home");
        routes.Add("/Home/Privacy");
        routes.Add("/Home/NotFoundError");
        routes.Add($"/Instructors/Details/{id}");
        // routes.Add($"/Instructors/Students/{courseId}");
        routes.Add("/Instructors/Grade");
        routes.Add("/Logout");

        return routes;
    }


    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var httpContext = filterContext.HttpContext;

        var token = httpContext.Session.GetString("_Token");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MY_SECRET_KEY_123456789_ABCDEFGHIJKLMNOPQRSTUVWXYZ"));


        if (!string.IsNullOrEmpty(token))
        {
            System.Console.WriteLine($"token: {token}");

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            DateTime exp = jwtSecurityToken.ValidTo;
            System.Console.WriteLine(DateTime.UtcNow);
            System.Console.WriteLine(exp);

            if (DateTime.Compare(exp, DateTime.UtcNow) <= 0)
            {
                System.Console.WriteLine("Expired Token");
                httpContext.Session.SetString("_Token", "");

                filterContext.Result = new RedirectToRouteResult(
                                            new RouteValueDictionary
                                            {
                                                { "Controller", "Login" },
                                                { "Action", "" }
                                            });
                return;

            }


            var userId = jwtSecurityToken.Claims.First(claim => claim.Type == "Id").Value;
            var userName = jwtSecurityToken.Claims.First(claim => claim.Type == "UserName").Value;
            var userRole = jwtSecurityToken.Claims.First(claim => claim.Type == "Role").Value;

            System.Console.WriteLine($"token_user_id: {userId}");
            System.Console.WriteLine($"token_username: {userName}");
            System.Console.WriteLine($"token_role: {userRole}");


            httpContext.Items["userId"] = userId;
            httpContext.Items["userName"] = userName;
            httpContext.Items["userRole"] = userRole;

            var route = httpContext.Request.Path.Value;

            System.Console.WriteLine($"Route: {route}");

            if (userRole == "Admin")
            {
                return;
            }
            else if (userRole == "Student")
            {
                var allowedRoutes = GetStudentAllowedRoutes(Int32.Parse(userId));

                if (allowedRoutes.Contains(route))
                {
                    return;
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                                           new RouteValueDictionary
                                           {
                                                { "Controller", "Home" },
                                                { "Action", "NotFoundError" }
                                           });
                    return;

                }

            }
            else if (userRole == "Instructor")
            {

                var allowedRoutes = GetInstructorAllowedRoutes(Int32.Parse(userId));

                if (allowedRoutes.Contains(route))
                {
                    return;
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                                           new RouteValueDictionary
                                           {
                                                { "Controller", "Home" },
                                                { "Action", "NotFoundError" }
                                           });
                    return;

                }
            }



        }
        else
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
