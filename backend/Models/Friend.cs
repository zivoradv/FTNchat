using System;

namespace FTNchat.Models
{
    public partial class Friend
    {
        public int FriendshipID { get; set; }

        public int UserID { get; set; }

        public int FriendID { get; set; }

        public string? Status { get; set; } // e.g., pending, accepted, declined

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual User? User { get; set; }

        public virtual User? FriendUser { get; set; }
    }
}
