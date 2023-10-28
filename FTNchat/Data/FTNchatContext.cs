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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
