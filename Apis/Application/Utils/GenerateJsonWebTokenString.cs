using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Utils
{
    public static class GenerateJsonWebTokenString
    {
        // public static string GenerateJsonWebToken(this User user, string secretKey, DateTime now)
        // {
        //     var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        //     var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //     var token = new JwtSecurityToken(
               
        //         expires: now.AddMinutes(15),
        //         signingCredentials: credentials);


        //     return new JwtSecurityTokenHandler().WriteToken(token);
        // }
    }
}
