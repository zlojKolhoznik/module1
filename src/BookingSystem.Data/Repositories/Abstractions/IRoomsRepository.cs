// <copyright file="IRoomsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Data.Repositories.Abstractions;

using BookingSystem.Data.Models;

/// <summary>
/// Abstracts operations on the Room entity.
/// </summary>
public interface IRoomsRepository
{
    /// <summary>
    /// Gets all rooms in specified hotel.
    /// </summary>
    /// <param name="hotelId">Unique identifier of the hotel.</param>
    /// <returns>IEnumerable of rooms which have HotelId set to <paramref name="hotelId"/>.</returns>
    Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(Guid hotelId);

    /// <summary>
    /// Gets room by its unique identifier.
    /// </summary>
    /// <param name="roomId">Unique identifier of the room.</param>
    /// <returns>Room entity from database.</returns>
    Task<Room> GetRoomByIdAsync(Guid roomId);

    /// <summary>
    /// Adds new room to the database.
    /// </summary>
    /// <param name="room">Room to be added.</param>
    /// <returns>Task.</returns>
    Task AddRoomAsync(Room room);

    /// <summary>
    /// Updates room in the database.
    /// </summary>
    /// <param name="room">Room to be updated.</param>
    /// <returns>Task.</returns>
    Task UpdateRoomAsync(Room room);

    /// <summary>
    /// Deletes room from the database.
    /// </summary>
    /// <param name="roomId">Unique identifier of the room.</param>
    /// <returns>Task.</returns>
    Task DeleteRoomAsync(Guid roomId);
}
