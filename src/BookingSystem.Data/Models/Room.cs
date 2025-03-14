// <copyright file="Room.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Data.Models;

/// <summary>
/// Represents a room.
/// </summary>
public class Room
{
    /// <summary>
    /// Gets or sets unique room identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets room number within the hotel.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Gets or sets room type.
    /// </summary>
    public RoomType Type { get; set; }

    /// <summary>
    /// Gets or sets room capacity.
    /// </summary>
    public RoomCapacity Capacity { get; set; }

    /// <summary>
    /// Gets or sets id of the hotel the room belongs to.
    /// </summary>
    public Guid HotelId { get; set; }

    /// <summary>
    /// Gets or sets hotel the room belongs to.
    /// </summary>
    public Hotel? Hotel { get; set; }

    /// <summary>
    /// Gets or sets bookings made for the room.
    /// </summary>
    public List<Booking> Bookings { get; set; } = [];
}