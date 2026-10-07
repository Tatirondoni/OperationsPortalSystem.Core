using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OperationsPortal.Infrastructure.Data.Entities;

namespace OperationsPortal.Infrastructure.Data;

public partial class OperationsPortalDbContext : DbContext
{
    public OperationsPortalDbContext(DbContextOptions<OperationsPortalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketHistory> TicketHistories { get; set; }

    public virtual DbSet<TicketStatus> TicketStatuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.Name, "UQ_Categories_Name").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Title).HasMaxLength(150);

            entity.HasOne(d => d.AssignedToUser).WithMany(p => p.TicketAssignedToUsers)
                .HasForeignKey(d => d.AssignedToUserId)
                .HasConstraintName("FK_Tickets_AssignedToUser");

            entity.HasOne(d => d.Category).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Category");

            entity.HasOne(d => d.OpenedByUser).WithMany(p => p.TicketOpenedByUsers)
                .HasForeignKey(d => d.OpenedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_OpenedByUser");

            entity.HasOne(d => d.TicketStatus).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TicketStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_TicketStatus");
        });

        modelBuilder.Entity<TicketHistory>(entity =>
        {
            entity.ToTable("TicketHistory");

            entity.Property(e => e.ChangedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Comment).HasMaxLength(1000);

            entity.HasOne(d => d.ChangedByUser).WithMany(p => p.TicketHistories)
                .HasForeignKey(d => d.ChangedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketHistory_ChangedByUser");

            entity.HasOne(d => d.NewTicketStatus).WithMany(p => p.TicketHistoryNewTicketStatuses)
                .HasForeignKey(d => d.NewTicketStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketHistory_NewTicketStatus");

            entity.HasOne(d => d.PreviousTicketStatus).WithMany(p => p.TicketHistoryPreviousTicketStatuses)
                .HasForeignKey(d => d.PreviousTicketStatusId)
                .HasConstraintName("FK_TicketHistory_PreviousTicketStatus");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketHistories)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketHistory_Ticket");
        });

        modelBuilder.Entity<TicketStatus>(entity =>
        {
            entity.ToTable("TicketStatus");

            entity.HasIndex(e => e.Name, "UQ_TicketStatus_Name").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.HasIndex(e => e.Login, "UQ_Users_Login").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
