using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FTNchat.Data;
using FTNchat.Models;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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
        public async Task<ActionResult<string>> GetUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                var jsonUsers = JsonSerializer.Serialize(users);
                return jsonUsers;
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "Error occurred while retrieving users.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var jsonUser = JsonSerializer.Serialize(user);
            return jsonUser;
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

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
