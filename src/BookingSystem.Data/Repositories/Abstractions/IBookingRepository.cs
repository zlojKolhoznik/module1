// <copyright file="IBookingRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace BookingSystem.Data.Repositories.Abstractions;

using BookingSystem.Data.Models;

/// <summary>
/// Abstracts operations on the Booking entity.
/// </summary>
public interface IBookingRepository
{
    /// <summary>
    /// Gets all bookings.
    /// </summary>
    /// <returns>Collection of bookings.</returns>
    Task<IEnumerable<Booking>> GetAllAsync();

    /// <summary>
    /// Gets a booking by its id.
    /// </summary>
    /// <param name="id">The booking id.</param>
    /// <returns>The booking.</returns>
    Task<Booking> GetByIdAsync(Guid id);

    /// <summary>
    /// Gets all bookings for a room.
    /// </summary>
    /// <param name="roomId">The room id.</param>
    /// <returns>Collection of bookings.</returns>
    Task<IEnumerable<Booking>> GetByRoomIdAsync(Guid roomId);

    /// <summary>
    /// Adds a booking.
    /// </summary>
    /// <param name="booking">The booking to add.</param>
    /// <returns>Task.</returns>
    Task AddAsync(Booking booking);

    /// <summary>
    /// Updates a booking.
    /// </summary>
    /// <param name="booking">The booking to update.</param>
    /// <returns>Task.</returns>
    Task UpdateAsync(Booking booking);

    /// <summary>
    /// Deletes a booking.
    /// </summary>
    /// <param name="id">The booking id.</param>
    /// <returns>Task.</returns>
    Task DeleteAsync(Guid id);
}
