// <copyright file="ApplicationDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Data;

using BookingSystem.Data.Abstractions;
using BookingSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

/// <inheritdoc/>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
 : DbContext(options), IApplicationDbContext
{
    /// <summary>
    /// Gets or sets the hotels database set.
    /// </summary>
    public required DbSet<Hotel> Hotels { get; set; }

    /// <summary>
    /// Gets or sets the rooms database set.
    /// </summary>
    public required DbSet<Room> Rooms { get; set; }

    /// <summary>
    /// Gets or sets the bookings database set.
    /// </summary>
    public required DbSet<Booking> Bookings { get; set; }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureHotelEntity(modelBuilder);
        ConfigureRoomEntity(modelBuilder);
        ConfigureBookingEntity(modelBuilder);
    }

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    private static void ConfigureRoomEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>(e =>
        {
            e.HasKey(r => r.Id);
            e.HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId);
            e.HasMany(r => r.Bookings)
                .WithOne(b => b.Room)
                .HasForeignKey(b => b.RoomId);
        });
    }

    private static void ConfigureHotelEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hotel>(e =>
        {
            e.HasKey(h => h.Id);
            e.Property(h => h.Name).IsRequired()
                .HasMaxLength(100);
            e.Property(h => h.Address).IsRequired();
            e.HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureBookingEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(e =>
        {
            e.HasKey(b => b.Id);
            e.Property(b => b.TenantName).IsRequired()
                .HasMaxLength(100);
            e.Property(b => b.TenantPassportNumber).IsRequired()
                .HasMaxLength(20);
            e.Property(b => b.TenantPhoneNumber).IsRequired()
                .HasMaxLength(20);
            e.Property(b => b.Start).IsRequired();
            e.Property(b => b.End).IsRequired();
            e.HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
