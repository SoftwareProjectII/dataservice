using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dataservice
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ui8pg832di94g954id95deci03"));

            var claims = new Claim[] {
                new Claim(ClaimTypes.Role, "HR"),
                new Claim(JwtRegisteredClaimNames.Email, "john.doe@blinkingcaret.com")};

            var token = new JwtSecurityToken(
                issuer: "Dataservice",
                audience: "Java app",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(28),
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256));
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
