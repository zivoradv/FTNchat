using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FTNchat.Data;
using FTNchat.Models;
using System;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using System.Linq;

namespace FTNchat.Controllers
{
    [Route("api/auth/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly FTNchatContext _context;

        public LoginController(FTNchatContext context)
        {
            _context = context;
        }

        // POST: api/auth/login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest loginRequest)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);

                if (user == null)
                {
                    return Unauthorized("Invalid email or password.");
                }

                string hashedPassword = HashPassword(loginRequest.Password);

                if (hashedPassword != user.Password)
                {
                    return Unauthorized("Invalid email or password.");
                }

                return Ok(new
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Username = user.Username,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex);
            }
        }


        private string HashPassword(string password)
        {
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: new byte[0], // Empty salt
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashedPassword;
        }

        public class UserLoginRequest
        {
            public string? Email { get; set; }
            public string? Password { get; set; }
        }


    }
}
