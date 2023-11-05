using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FTNchat.Data;
using FTNchat.Models;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using System;

namespace FTNchat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly ILogger<FriendsController> _logger;
        private readonly FTNchatContext _context;

        public FriendsController(FTNchatContext context, ILogger<FriendsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Friends
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friend>>> GetFriends()
        {
            try
            {
                var friends = await _context.Friends.ToListAsync();
                return friends;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving friends.");
                return StatusCode(500, "Internal Server Error");
            }
        }


        // GET: api/Friends/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Friend>> GetFriend(int id)
        {
            var friend = await _context.Friends.FindAsync(id);

            if (friend == null)
            {
                return NotFound();
            }

            return friend;
        }

        // POST: api/Friends
        [EnableCors("origin")]
        [HttpPost]
        public async Task<IActionResult> PostFriend([FromBody] Friend friend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Friends.Add(friend);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetFriend), new { id = friend.FriendshipID }, friend);
            }
            catch (DbUpdateException ex)
            {
                // Handle unique constraint violation
                if (ex.InnerException is MySql.Data.MySqlClient.MySqlException mysqlEx && mysqlEx.Number == 1062)
                {
                    return Conflict("Friendship already exists.");
                }
                else
                {
                    _logger.LogError(ex, "Error occurred while saving friendship.");
                    return StatusCode(500, "Internal Server Error");
                }
            }
        }


        // PUT: api/Friends/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriend(int id, [FromBody] Friend friend)
        {
            if (id != friend.FriendshipID)
            {
                return BadRequest();
            }

            _context.Entry(friend).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendExists(id))
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

        // DELETE: api/Friends/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriend(int id)
        {
            var friend = await _context.Friends.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }

            _context.Friends.Remove(friend);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FriendExists(int id)
        {
            return _context.Friends.Any(e => e.FriendshipID == id);
        }
    }
}
