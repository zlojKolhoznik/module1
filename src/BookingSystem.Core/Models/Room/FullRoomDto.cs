// <copyright file="FullRoomDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Core.Models.Room;

using BookingSystem.Core.Models.Booking;
using BookingSystem.Core.Models.Hotel;
using BookingSystem.Data.Models;

/// <summary>
/// Represents a full room entity.
/// </summary>
public class FullRoomDto
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
    /// Gets or sets the brief info about the hotel the room belongs to.
    /// </summary>
    public required BriefHotelDto Hotel { get; set; }

    /// <summary>
    /// Gets or sets bookings made for the room.
    /// </summary>
    public List<BriefBookingDto> Bookings { get; set; } = [];
}
