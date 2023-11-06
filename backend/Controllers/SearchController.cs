using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FTNchat.Data;
using FTNchat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTNchat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly FTNchatContext _context;

        public SearchController(FTNchatContext context)
        {
            _context = context;
        }

        // GET: api/Search
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> SearchUsers([FromQuery] string q)
        {
            try
            {
                if (string.IsNullOrEmpty(q))
                {
                    return BadRequest("Query parameter is required.");
                }

                var users = await _context.Users
                    .Where(user =>
                        user.Username.Contains(q) ||
                        user.Email.Contains(q))
                    .ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
