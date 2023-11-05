using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FTNchat.Data;
using FTNchat.Models;

namespace FTNchat.Controllers
{
    [Route("api/chatmessages")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
        private readonly FTNchatContext _context;

        public ChatMessagesController(FTNchatContext context)
        {
            _context = context;
        }

        // GET: api/chatmessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chatmessage>>> GetChatMessages()
        {
            var chatMessages = await _context.Chatmessages.ToListAsync();
            return chatMessages;
        }

        // GET: api/chatmessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chatmessage>> GetChatMessage(int id)
        {
            var chatMessage = await _context.Chatmessages.FindAsync(id);

            if (chatMessage == null)
            {
                return NotFound();
            }

            return chatMessage;
        }

        // POST: api/chatmessages
        [HttpPost]
        public async Task<ActionResult<Chatmessage>> CreateChatMessage([FromBody] Chatmessage message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            message.Timestamp = DateTime.Now;
            _context.Chatmessages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChatMessage), new { id = message.MessageId }, message);
        }

        // PUT: api/chatmessages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChatMessage(int id, [FromBody] Chatmessage updatedMessage)
        {
            if (id != updatedMessage.MessageId)
            {
                return BadRequest();
            }

            _context.Entry(updatedMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatMessageExists(id))
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

        // DELETE: api/chatmessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatMessage(int id)
        {
            var chatMessage = await _context.Chatmessages.FindAsync(id);
            if (chatMessage == null)
            {
                return NotFound();
            }

            _context.Chatmessages.Remove(chatMessage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChatMessageExists(int id)
        {
            return _context.Chatmessages.Any(e => e.MessageId == id);
        }
    }
}
