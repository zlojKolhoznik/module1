// <copyright file="IHotelsService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Core.Services.Abstractions;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingSystem.Core.Models.Hotel;

/// <summary>
/// Service for managing hotels.
/// </summary>
public interface IHotelsService
{
    /// <summary>
    /// Creates a new hotel.
    /// </summary>
    /// <param name="hotel">Data transfer object for hotel creation.</param>
    /// <returns>Task.</returns>
    Task AddHotelAsync(CreateHotelDto hotel);

    /// <summary>
    /// Gets a hotel by id.
    /// </summary>
    /// <param name="id">ID of the hotel to be fetched.</param>
    /// <returns>Data transfer object that represents full hotel.</returns>
    Task<FullHotelDto> GetHotelAsync(Guid id);

    /// <summary>
    /// Gets all hotels.
    /// </summary>
    /// <returns>A collection of data transfer objects that represent brief hotels.</returns>
    Task<IEnumerable<BriefHotelDto>> GetHotelsAsync();

    /// <summary>
    /// Updates a hotel.
    /// </summary>
    /// <param name="id">ID of a hotel.</param>
    /// <param name="hotel">Data transfer object for updating a hotel.</param>
    /// <returns>Task.</returns>
    /// <remarks>
    /// If ID property of <paramref name="hotel"/> is not equal to <paramref name="id"/>,
    /// the method should throw an exception.
    /// </remarks>
    Task UpdateHotelAsync(Guid id, UpdateHotelDto hotel);

    /// <summary>
    /// Deletes a hotel.
    /// </summary>
    /// <param name="id">ID of a hotel to be deleted.</param>
    /// <returns>Task.</returns>
    Task DeleteHotelAsync(Guid id);
}
