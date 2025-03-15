// <copyright file="RoomsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Web.Controllers;

using BookingSystem.Core.Models.Room;
using BookingSystem.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Rooms controller.
/// </summary>
[Route("api/hotels/{hotelId:guid}/[controller]")]
[ApiController]
public class RoomsController(IRoomsService service) : ControllerBase
{
    /// <summary>
    /// Gets all rooms of the hotel.
    /// </summary>
    /// <param name="hotelId">Id of the hotel.</param>
    /// <returns>200 OK with colletion of rooms.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BriefRoomDto>>> GetRooms(Guid hotelId)
    {
        var rooms = await service.GetRoomsAsync(hotelId);
        return Ok(rooms);
    }

    /// <summary>
    /// Gets room by id.
    /// </summary>
    /// <param name="hotelId">Hotel id.</param>
    /// <param name="id">Room id.</param>
    /// <returns>200 OK with room if it exists and belongs to specified hotel,
    /// 400 Bad Request if room exists but belongs to another hotel,
    /// or 404 Not Found if room doesn't exist.</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FullRoomDto>> GetRoom(Guid hotelId, Guid id)
    {
        FullRoomDto room;
        try
        {
            room = await service.GetRoomAsync(id);
            if (room.Hotel.Id != hotelId)
            {
                return BadRequest("Room exists but belongs to another hotel.");
            }
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }

        return Ok(room);
    }

    /// <summary>
    /// Updates a room.
    /// </summary>
    /// <param name="id">Room Id.</param>
    /// <param name="room">Room update model.</param>
    /// <returns>204 No Content if room exists,
    /// 400 Bad Request if <paramref name="id"/> and <paramref name="room"/>.Id do not match,
    /// or 404 Not Found.</returns>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateRoom(
        Guid id,
        [FromBody] UpdateRoomDto room)
    {
        if (room.Id != id)
        {
            return BadRequest("ID in the URL and in the body do not match.");
        }

        try
        {
            await service.UpdateRoomAsync(id, room);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }

        return NoContent();
    }

    /// <summary>
    /// Adds a new room.
    /// </summary>
    /// <param name="hotelId">Rooms hotel id.</param>
    /// <param name="room">Room creation model.</param>
    /// <returns>400 Bad Request if <paramref name="hotelId"/>
    /// and <paramref name="room"/>.HotelId do not match or hotel does not exist,
    /// or 200 OK if room added successfully.</returns>
    [HttpPost]
    public async Task<ActionResult> AddRoom(Guid hotelId, [FromBody] CreateRoomDto room)
    {
        if (hotelId != room.HotelId)
        {
            return BadRequest("Hotel ID in the URL and in the body do not match.");
        }

        try
        {
            await service.AddRoomAsync(room);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }

    /// <summary>
    /// Deletes a room.
    /// </summary>
    /// <param name="id">Room id.</param>
    /// <returns>204 No Content if room deleted succesfully,
    /// or 404 Not Found if room doesn't exist.</returns>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteRoom(Guid id)
    {
        try
        {
            await service.DeleteRoomAsync(id);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }

        return NoContent();
    }
}
