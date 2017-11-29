using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Security.Claims;

using dataservice.Models;

namespace dataservice.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : Controller
    {
        private readonly _17SP2G4Context _context;
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration, _17SP2G4Context context)
        {
            _configuration = configuration;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TokenRequest login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string username = login.Username;
            string password = login.Password;

            User user = await _context.User.Where(m => m.Username == username).FirstOrDefaultAsync();

            if (user == null || user.Password != password)
            {
                return Unauthorized();
            }

            JwtSecurityToken token = getToken(username, password);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RequestToken([FromBody] TokenRequest request)
        {
            if (request.Username == "Jon" && request.Password == "Test")
            {
                JwtSecurityToken token = getToken(request.Username, request.Password);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return BadRequest("Could not verify username and password");
        }

        private JwtSecurityToken getToken(string username, string password)
        {
            var claims = new[]
                {
                new Claim(ClaimTypes.Name, username)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "dataservice",
                audience: "java app",
                claims: claims,
                signingCredentials: creds);

            return token;
        }
    }
    public class TokenRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}