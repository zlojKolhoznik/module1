// <copyright file="CreateHotelDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Core.Models.Hotel;

/// <summary>
/// Represents a data transfer object for creating a hotel.
/// </summary>
public class CreateHotelDto
{
    /// <summary>
    /// Gets or sets hotel name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets hotel address.
    /// </summary>
    public required string Address { get; set; }
}
