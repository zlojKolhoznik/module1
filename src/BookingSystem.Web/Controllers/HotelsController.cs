// <copyright file="HotelsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Web.Controllers;

using BookingSystem.Core.Models.Hotel;
using BookingSystem.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Hotels controller.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class HotelsController(IHotelsService service) : ControllerBase
{
    /// <summary>
    /// Gets all hotels.
    /// </summary>
    /// <returns>200 OK with collection of brief hotels.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BriefHotelDto>>> GetHotels()
    {
        var hotels = await service.GetHotelsAsync();
        return Ok(hotels);
    }

    /// <summary>
    /// Gets hotel by id.
    /// </summary>
    /// <param name="id">Hotel id.</param>
    /// <returns>200 OK with brief hotel if exists or 404 Not Found.</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FullHotelDto>> GetHotel(Guid id)
    {
        FullHotelDto hotel;
        try
        {
            hotel = await service.GetHotelAsync(id);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }

        return Ok(hotel);
    }

    /// <summary>
    /// Adds a new hotel.
    /// </summary>
    /// <param name="hotel">Hotel creation model.</param>
    /// <returns>200 OK.</returns>
    [HttpPost]
    public async Task<ActionResult> AddHotel([FromBody] CreateHotelDto hotel)
    {
        await service.AddHotelAsync(hotel);
        return Ok();
    }

    /// <summary>
    /// Updates a hotel.
    /// </summary>
    /// <param name="id">Hotel id.</param>
    /// <param name="hotel">Hotel update model.</param>
    /// <returns>204 No Content if hotel exists or 404 Not Found.</returns>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateHotel(Guid id, [FromBody] UpdateHotelDto hotel)
    {
        try
        {
            await service.UpdateHotelAsync(id, hotel);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }

        return NoContent();
    }

    /// <summary>
    /// Deletes a hotel.
    /// </summary>
    /// <param name="id">Hotel id.</param>
    /// <returns>204 No Content if hotel exists or 404 Not Found.</returns>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteHotel(Guid id)
    {
        try
        {
            await service.DeleteHotelAsync(id);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }

        return NoContent();
    }
}
