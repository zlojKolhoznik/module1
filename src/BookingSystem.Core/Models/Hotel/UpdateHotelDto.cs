// <copyright file="UpdateHotelDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Core.Models.Hotel;

/// <summary>
/// Represents a data transfer object for updating a hotel.
/// </summary>
public class UpdateHotelDto
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
    /// Gets or sets hotel room list in form of IDs.
    /// </summary>
    public List<Guid> Rooms { get; set; } = [];
}
