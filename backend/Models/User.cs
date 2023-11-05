using System;
using System.Collections.Generic;

namespace FTNchat.Models
{
    public partial class User
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime? Birthday { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Chatmessage> ChatmessageReceivers { get; set; } = new List<Chatmessage>();

        public virtual ICollection<Chatmessage> ChatmessageSenders { get; set; } = new List<Chatmessage>();
    
         public virtual ICollection<Friend> Friends { get; set; } = new List<Friend>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
