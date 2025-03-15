// <copyright file="IBookingService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Core.Services.Abstractions;

using BookingSystem.Core.Models.Booking;

/// <summary>
/// Service for managing bookings.
/// </summary>
public interface IBookingService
{
    /// <summary>
    /// Creates a new booking.
    /// </summary>
    /// <param name="booking">Data tranfer object for booking creation.</param>
    /// <returns>Task.</returns>
    Task AddBookingAsync(CreateBookingDto booking);

    /// <summary>
    /// Gets a booking by id.
    /// </summary>
    /// <param name="id">ID of the booking to be fetched.</param>
    /// <returns>Data transfer object that represents full booking.</returns>
    Task<FullBookingDto> GetBookingAsync(Guid id);

    /// <summary>
    /// Gets all bookings.
    /// </summary>
    /// <returns>A collection of data transfer objects that represent brief bookings.</returns>
    Task<IEnumerable<BriefBookingDto>> GetBookingsAsync();

    /// <summary>
    /// Updates a booking.
    /// </summary>
    /// <param name="id">ID of a booking.</param>
    /// <param name="booking">Data transfer object for updating a booking.</param>
    /// <returns>Task.</returns>
    /// <remarks>
    /// If ID property of <paramref name="booking"/> is not equal to <paramref name="id"/>,
    /// the method should throw an exception.
    /// </remarks>
    Task UpdateBookingAsync(Guid id, UpdateBookingDto booking);

    /// <summary>
    /// Deletes a booking.
    /// </summary>
    /// <param name="id">ID of a booking to be deleted.</param>
    /// <returns>Task.</returns>
    Task DeleteBookingAsync(Guid id);
}
