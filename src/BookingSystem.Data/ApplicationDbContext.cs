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

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureHotelEntity(modelBuilder);
        ConfigureRoomEntity(modelBuilder);
    }

    private static void ConfigureRoomEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>(e =>
        {
            e.HasKey(r => r.Id);
            e.HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId);
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
                .HasForeignKey(r => r.HotelId);
        });
    }
}
