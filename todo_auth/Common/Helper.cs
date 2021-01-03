using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace todo_auth.Common
{
    public class Helper
    {
        public static string CreateMD5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static (string token,DateTime exp) GenerateToken(int id, IConfiguration _config)
        {
            var seretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            var credentials = new SigningCredentials(seretKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Aud,id.ToString())
            };
            DateTime exp = DateTime.UtcNow.AddMinutes(Values.LIFECIRCLE_TOKEN_MINUTES);
            var token = new JwtSecurityToken(

                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:ValidAudience"],
                claims: claims,
                expires: exp,
                signingCredentials: credentials
                );
            return (new JwtSecurityTokenHandler().WriteToken(token), exp);
        }

        public static string GetTokenFromRequest(HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
                return null;
            return string.IsNullOrEmpty(token) ? null : token.Replace("Bearer ", "");
        }
    }
}
