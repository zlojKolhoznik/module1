// <copyright file="CreateRoomDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Core.Models.Room;

using BookingSystem.Data.Models;

/// <summary>
/// Data transfer object for creating a room.
/// </summary>
public class CreateRoomDto
{
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
}
