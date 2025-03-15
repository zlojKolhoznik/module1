// <copyright file="BriefHotelDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Core.Models.Hotel;

/// <summary>
/// Represents a brief hotel entity.
/// </summary>
public class BriefHotelDto
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
}