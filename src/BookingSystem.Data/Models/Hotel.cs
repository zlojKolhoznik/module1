// <copyright file="Hotel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Data.Models;

/// <summary>
/// Represents a hotel.
/// </summary>
public class Hotel
{
    /// <summary>
    /// Gets or sets unique hotel identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets hotel name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets hotel address.
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Gets or sets hotel room list.
    /// </summary>
    public List<Room> Rooms { get; set; } = [];
}
