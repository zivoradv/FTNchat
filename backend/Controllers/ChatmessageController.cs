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
    [Route("api/chatmessages")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
        private readonly List<Chatmessage> _chatMessages = new List<Chatmessage>();

        // GET: api/chatmessages
        [HttpGet]
        public ActionResult<IEnumerable<Chatmessage>> GetChatMessages()
        {
            // Return all chat messages
            return _chatMessages;
        }

        // GET: api/chatmessages/5
        [HttpGet("{id}")]
        public ActionResult<Chatmessage> GetChatMessage(int id)
        {
            // Find and return a chat message by its MessageId
            var chatMessage = _chatMessages.Find(m => m.MessageId == id);
            if (chatMessage == null)
            {
                return NotFound(); // 404 Not Found
            }
            return chatMessage;
        }

        // POST: api/chatmessages
        [HttpPost]
        public ActionResult<Chatmessage> CreateChatMessage([FromBody] Chatmessage message)
        {
            // Simulate generating a unique MessageId and setting the Timestamp
            message.MessageId = _chatMessages.Count + 1;
            message.Timestamp = DateTime.Now;

            // Add the new message to the list
            _chatMessages.Add(message);

            // Return a response with the created message and location
            return CreatedAtAction(nameof(GetChatMessage), new { id = message.MessageId }, message);
        }

        // PUT: api/chatmessages/5
        [HttpPut("{id}")]
        public IActionResult UpdateChatMessage(int id, [FromBody] Chatmessage updatedMessage)
        {
            // Find the message to update
            var existingMessage = _chatMessages.Find(m => m.MessageId == id);
            if (existingMessage == null)
            {
                return NotFound(); // 404 Not Found
            }

            // Update the properties of the existing message
            existingMessage.MessageText = updatedMessage.MessageText;
            existingMessage.Timestamp = DateTime.Now; // Update the timestamp

            // Return a 204 No Content response
            return NoContent();
        }

        // DELETE: api/chatmessages/5
        [HttpDelete("{id}")]
        public IActionResult DeleteChatMessage(int id)
        {
            // Find the message to delete
            var messageToRemove = _chatMessages.Find(m => m.MessageId == id);
            if (messageToRemove == null)
            {
                return NotFound(); // 404 Not Found
            }

            // Remove the message from the list
            _chatMessages.Remove(messageToRemove);

            // Return a 204 No Content response
            return NoContent();
        }
    }
}
