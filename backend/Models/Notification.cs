using System;

namespace FTNchat.Models
{
    public partial class Notification
    {
        public int NotificationID { get; set; }

        public int UserID { get; set; }

        public string? Message { get; set; }

        public string? Link { get; set; } // URL related to the notification (if applicable)

        public bool ReadStatus { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual User? User { get; set; }
    }
}
