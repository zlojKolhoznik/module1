// <copyright file="IHotelsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Data.Repositories.Abstractions;

using BookingSystem.Data.Models;

/// <summary>
/// Abstracts operations on the Hotel entity.
/// </summary>
public interface IHotelsRepository
{
    /// <summary>
    /// Gets all hotels from the database.
    /// </summary>
    /// <returns>IEnumerable that contains all the hotels in DB.</returns>
    Task<IEnumerable<Hotel>> GetHotelsAsync();

    /// <summary>
    /// Gets hotel by its unique identifier.
    /// </summary>
    /// <param name="hotelId">Unique identifier of the hotel.</param>
    /// <returns>Hotel entity.</returns>
    Task<Hotel> GetHotelByIdAsync(Guid hotelId);

    /// <summary>
    /// Adds new hotel to the database.
    /// </summary>
    /// <param name="hotel">Hotel to add.</param>
    /// <returns>Task.</returns>
    Task AddHotelAsync(Hotel hotel);

    /// <summary>
    /// Updates hotel in the database.
    /// </summary>
    /// <param name="hotel">Hotel to be updated.</param>
    /// <returns>Task.</returns>
    Task UpdateHotelAsync(Hotel hotel);

    /// <summary>
    /// Deletes hotel from the database.
    /// </summary>
    /// <param name="hotelId">Unique identifier of the hotel to be deleted.</param>
    /// <returns>Task.</returns>
    Task DeleteHotelAsync(Guid hotelId);
}
