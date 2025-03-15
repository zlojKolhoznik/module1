// <copyright file="IRoomsService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Core.Services.Abstractions;

using System.Collections.Generic;
using System.Threading.Tasks;
using BookingSystem.Core.Models.Room;

/// <summary>
/// Service for managing rooms.
/// </summary>
public interface IRoomsService
{
    /// <summary>
    /// Creates a new room.
    /// </summary>
    /// <param name="room">Data transfer object for room creation.</param>
    /// <returns>Task.</returns>
    Task AddRoomAsync(CreateRoomDto room);

    /// <summary>
    /// Gets a room by id.
    /// </summary>
    /// <param name="id">ID of the room to be fetched.</param>
    /// <returns>Data transfer object that represents full room.</returns>
    Task<FullRoomDto> GetRoomAsync(Guid id);

    /// <summary>
    /// Gets all rooms.
    /// </summary>
    /// <param name="hotelId">ID of the hotel to get rooms for.</param>
    /// <returns>A collection of data transfer objects that represent brief rooms.</returns>
    Task<IEnumerable<BriefRoomDto>> GetRoomsAsync(Guid hotelId);

    /// <summary>
    /// Updates a room.
    /// </summary>
    /// <param name="id">ID of a room.</param>
    /// <param name="room">Data transfer object for updating a room.</param>
    /// <returns>Task.</returns>
    /// <remarks>
    /// If ID property of <paramref name="room"/> is not equal to <paramref name="id"/>,
    /// the method should throw an exception.
    /// </remarks>
    Task UpdateRoomAsync(Guid id, UpdateRoomDto room);

    /// <summary>
    /// Deletes a room.
    /// </summary>
    /// <param name="id">ID of a room to be deleted.</param>
    /// <returns>Task.</returns>
    Task DeleteRoomAsync(Guid id);
}
