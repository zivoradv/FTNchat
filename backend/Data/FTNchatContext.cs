using System;
using System.Collections.Generic;
using FTNchat.Models;
using Microsoft.EntityFrameworkCore;

namespace FTNchat.Data;

public partial class FTNchatContext : DbContext
{
    public FTNchatContext(DbContextOptions<FTNchatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chatmessage> Chatmessages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Friend> Friends { get; set; }
    public virtual DbSet<Notification> Notifications { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Chatmessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PRIMARY");

            entity.ToTable("chatmessages");

            entity.HasIndex(e => e.ReceiverId, "ReceiverID");

            entity.HasIndex(e => e.SenderId, "SenderID");

            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.MessageText).HasColumnType("text");
            entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");
            entity.Property(e => e.SenderId).HasColumnName("SenderID");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");

            entity.HasOne(d => d.Receiver).WithMany(p => p.ChatmessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .HasConstraintName("chatmessages_ibfk_2");

            entity.HasOne(d => d.Sender).WithMany(p => p.ChatmessageSenders)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("chatmessages_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

         modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => e.FriendshipID).HasName("PRIMARY");

                entity.ToTable("friends");

                entity.Property(e => e.FriendshipID).HasColumnName("FriendshipID");
                entity.Property(e => e.UserID).HasColumnName("UserID");
                entity.Property(e => e.FriendID).HasColumnName("FriendID");
                entity.Property(e => e.Status).HasMaxLength(50);
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("timestamp");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Friends)
                    .HasForeignKey(d => d.UserID)
                    .HasConstraintName("FK_Friends_Users_UserID");

                entity.HasOne(d => d.FriendUser)
                    .WithMany()
                    .HasForeignKey(d => d.FriendID)
                    .HasConstraintName("FK_Friends_Users_FriendID");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.NotificationID).HasName("PRIMARY");

                entity.ToTable("notifications");

                entity.Property(e => e.NotificationID).HasColumnName("NotificationID");
                entity.Property(e => e.UserID).HasColumnName("UserID");
                entity.Property(e => e.Message).HasColumnType("text");
                entity.Property(e => e.Link).HasMaxLength(255);
                entity.Property(e => e.ReadStatus).HasColumnType("bit");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("timestamp");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserID)
                    .HasConstraintName("FK_Notifications_Users_UserID");
            });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
