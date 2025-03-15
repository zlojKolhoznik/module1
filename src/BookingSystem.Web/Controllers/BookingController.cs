// <copyright file="BookingController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Web.Controllers;

using BookingSystem.Core.Models.Booking;
using BookingSystem.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Booking controller.
/// </summary>
[Route("api/rooms/{roomId:guid}/book")]
[ApiController]
public class BookingController(IBookingService service) : ControllerBase
{
    /// <summary>
    /// Gets all bookings for the room.
    /// </summary>
    /// <param name="roomId">Room id.</param>
    /// <returns>200 OK with booking colletion (if room doesn't exist, the collection is empty).</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BriefBookingDto>>> GetRoomBookings(Guid roomId)
    {
        var bookings = await service.GetBookingsAsync();
        return Ok(bookings.Where(b => b.RoomId == roomId));
    }

    /// <summary>
    /// Books a room.
    /// </summary>
    /// <param name="roomId">Room id.</param>
    /// <param name="booking">Booking model.</param>
    /// <returns>200 OK if booking was successful,
    /// 400 Bad Request if room is already booked for specified period or doesn't exist,
    /// or differs from one specified in model.</returns>
    [HttpPost]
    public async Task<ActionResult> BookRoom(Guid roomId, [FromBody] CreateBookingDto booking)
    {
        if (booking.RoomId != roomId)
        {
            return BadRequest("Room mismatch.");
        }

        try
        {
            await service.AddBookingAsync(booking);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }

    /// <summary>
    /// Cancels a booking.
    /// </summary>
    /// <param name="roomId">Room that is booked.</param>
    /// <param name="id">Booking id.</param>
    /// <returns>200 OK if booking cancellation successfull,
    /// 400 Bad Request if wrong room is provided,
    /// or 404 Not Found if booking doesn't exist.</returns>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> CancelBooking(Guid roomId, Guid id)
    {
        var booking = await service.GetBookingAsync(id);
        if (booking.Room.Id != roomId)
        {
            return BadRequest("Wrong room.");
        }

        try
        {
            await service.DeleteBookingAsync(id);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }

        return Ok();
    }

    /// <summary>
    /// Updates a booking.
    /// </summary>
    /// <param name="id">Booking id.</param>
    /// <param name="booking">Booking update model.</param>
    /// <returns>400 Bad Request if <paramref name="id"/> isn't equal to <paramref name="booking"/>.Id,
    /// 404 Not Found if booking doens't exist or room isn't available for specified period,
    /// or 204 No Content if update successfull.</returns>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateBooking(Guid id, [FromBody] UpdateBookingDto booking)
    {
        try
        {
            await service.UpdateBookingAsync(id, booking);
        }
        catch (ArgumentException e)
        {
            return e.Message.Contains("match", StringComparison.InvariantCultureIgnoreCase)
                ? BadRequest(e.Message)
                : NotFound(e.Message);
        }

        return NoContent();
    }

    /// <summary>
    /// Gets all bookings.
    /// </summary>
    /// <returns>200 OK with collection of all bookings.</returns>
    [HttpGet("/api/bookings")]
    public async Task<ActionResult<IEnumerable<BriefBookingDto>>> GetBookings()
    {
        var bookings = await service.GetBookingsAsync();
        return Ok(bookings);
    }

    /// <summary>
    /// Gets booking by id.
    /// </summary>
    /// <param name="id">Booking id.</param>
    /// <returns>200 OK if booking exists or 404 Not Found if not.</returns>
    [HttpGet("/api/bookings/{id:guid}")]
    public async Task<ActionResult<FullBookingDto>> GetBooking(Guid id)
    {
        FullBookingDto booking;
        try
        {
            booking = await service.GetBookingAsync(id);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }

        return Ok(booking);
    }
}
