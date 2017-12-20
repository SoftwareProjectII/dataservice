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
using System;

namespace dataservice.Controllers
{
    [Authorize]
    [AllowAnonymous]
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

            return Ok(new TokenAndUsername
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                userid = user.UserId
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
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            return token;
        }

        [HttpGet("dummy")]
        public IActionResult MakeDummyData(bool loadtrainings = false)
        {
            Random rnd = new Random();
            if (loadtrainings)
            {
                for (int i = 1; i <= 100; i++)
                {                    
                    Traininginfo ti = new Traininginfo
                    {
                        Name = $"Trainingnaam {i}",
                        InfoGeneral = $"General info {i}",
                        NumberOfDays = rnd.Next(1, 5),
                        InfoExam = $"Exam info {i}",
                        InfoPayment = $"Payment info {i}",
                        Price = rnd.NextDouble() * rnd.Next(0, 200)
                    };
                    ti.Survey = new Survey();
                    for (int j = 1; j <= rnd.Next(3, 8); j++)
                    {
                        ti.Survey.Surveyquestion.Add(new Surveyquestion { Content = $"Survey question {j + i * 5}" });
                    }
                    for (int j = 1; j <= rnd.Next(0, 4); j++)
                    {
                        TimeSpan start = new TimeSpan(rnd.Next(0, 16), rnd.Next(0, 59), rnd.Next(0, 59));
                        int index = rnd.Next(_context.Address.Count());
                        ti.Trainingsession.Add(new Trainingsession
                        {
                            Address = _context.Address.Skip(index).FirstOrDefault(),
                            Teacher = new Teacher
                            {
                                FirstName = $"First name {j}",
                                LastName = $"Last name {j}",
                                PhoneNumber = rnd.Next(100000000, 999999999).ToString(),
                                Email = $"Email {j}"
                            },
                            Date = DateTime.Now.Date.AddDays(rnd.Next(-1, 30)),
                            StartHour = start,
                            EndHour = start.Add(new TimeSpan(rnd.Next(1, 4))),
                            Cancelled = rnd.Next(0, 1) == 1
                        });
                    }
                    for (int j = 1; j <= rnd.Next(0, 10); j++)
                    {
                        ti.Trainingfaq.Add(new Trainingfaq
                        {
                            Faq = new Faq
                            {
                                QuestionFaq = $"Question {j}",
                                AnswerFaq = $"Answer {j}"
                            }
                        });
                    }
                    _context.Traininginfo.Add(ti);
                }
            }

            _context.SaveChanges();
            return Ok();
        }
    }
    public class TokenRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class TokenAndUsername
    {
        public string token { get; set; }
        public int userid { get; set; }
    }
}