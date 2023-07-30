using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace online_course_platform.Utilities;

public class Authentication
{

    public static string GenerateJwtToken(int id, string userName, string role)
    {
        var claims = new List<Claim> {
            new Claim("Id", id.ToString()),
            new Claim("UserName", userName),
            new Claim("Role", role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MY_SECRET_KEY_123456789_ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddMinutes(5);

        var token = new JwtSecurityToken(issuer: "http://localhost", claims: claims, expires: expires, signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
