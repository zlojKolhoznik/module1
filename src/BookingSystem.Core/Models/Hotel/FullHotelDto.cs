// <copyright file="FullHotelDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Core.Models.Hotel;

using BookingSystem.Core.Models.Room;

/// <summary>
/// Represents a full hotel entity.
/// </summary>
public class FullHotelDto
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
    public List<BriefRoomDto> Rooms { get; set; } = [];
}
