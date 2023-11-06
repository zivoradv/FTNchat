using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FTNchat.Data;
using FTNchat.Models;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using System.Threading.Tasks;

namespace FTNchat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly FTNchatContext _context;

        public UsersController(FTNchatContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving users.");
                return StatusCode(500, "Internal Server Error");
            }
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        // POST: api/Users
        [EnableCors("origin")]
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.Password = HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user.Password = null;

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
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

        // POST: api/Users/SearchUsers
        // [HttpPost("SearchUsers")]
        // public async Task<ActionResult<IEnumerable<User>>> SearchUsers([FromBody] string query)
        // {
        //     try
        //     {
        //         if (string.IsNullOrEmpty(query))
        //         {
        //             return BadRequest("Query parameter is required.");
        //         }

        //         var users = await _context.Users
        //             .Where(user =>
        //                 user.Username.Contains(query) ||
        //                 user.Email.Contains(query))
        //             .ToListAsync();

        //         return Ok(users);
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error occurred while searching users.");
        //         return StatusCode(500, "Internal Server Error");
        //     }
        // }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
