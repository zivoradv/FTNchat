using System;

namespace FTNchat.Models
{
    public partial class Chatmessage
    {
        public int MessageId { get; set; }

        public int? SenderId { get; set; }

        public int? ReceiverId { get; set; }

        public string? MessageText { get; set; }

        public string? MessageStatus { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? ReadTimestamp { get; set; }

        public DateTime? Timestamp { get; set; }

        public virtual User? Receiver { get; set; }

        public virtual User? Sender { get; set; }
    }
}
