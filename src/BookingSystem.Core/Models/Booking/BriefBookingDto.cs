// <copyright file="BriefBookingDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Core.Models.Booking;

/// <summary>
/// Represents a brief booking entity.
/// </summary>
public class BriefBookingDto
{
    /// <summary>
    /// Gets or sets the booking id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the tenant name.
    /// </summary>
    public required string TenantName { get; set; }

    /// <summary>
    /// Gets or sets the tenant passport number.
    /// </summary>
    public required string TenantPassportNumber { get; set; }

    /// <summary>
    /// Gets or sets the tenant phone number.
    /// </summary>
    public required string TenantPhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the booking start date.
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// Gets or sets the booking end date.
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// Gets or sets the room id.
    /// </summary>
    public Guid RoomId { get; set; }
}
