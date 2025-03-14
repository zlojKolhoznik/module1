// <copyright file="HotelsRepositoryTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Data.Tests;

using BookingSystem.Data.Abstractions;
using BookingSystem.Data.Models;
using BookingSystem.Data.Repositories;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

public class HotelsRepositoryTests
{
    private readonly Mock<IApplicationDbContext> _mockContext;
    private readonly HotelsRepository _repository;

    public HotelsRepositoryTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        _repository = new HotelsRepository(_mockContext.Object);
    }

    [Fact]
    public async Task AddHotelAsync_ShouldAddHotel()
    {
        var hotel = new Hotel
        {
            Id = Guid.NewGuid(),
            Name = "Hotel",
            Address = "Address",
        };
        _mockContext.Setup(m => m.Set<Hotel>().AddAsync(hotel, It.IsAny<CancellationToken>()))
            .Verifiable();

        await _repository.AddHotelAsync(hotel);

        _mockContext.Verify(m => m.Set<Hotel>().AddAsync(hotel, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteHotelAsync_ShouldDeleteHotel_WhenHotelExists()
    {
        var hotelId = Guid.NewGuid();
        var hotel = new Hotel
        {
            Id = hotelId,
            Name = "Hotel",
            Address = "Address",
        };

        _mockContext.Setup(m => m.Set<Hotel>().FindAsync(hotelId)).ReturnsAsync(hotel);

        await _repository.DeleteHotelAsync(hotelId);

        _mockContext.Verify(m => m.Set<Hotel>().Remove(hotel), Times.Once);
    }

    [Fact]
    public async Task DeleteHotelAsync_ShouldThrowArgumentException_WhenHotelDoesNotExist()
    {
        var hotelId = Guid.NewGuid();

        _mockContext.Setup(m => m.Set<Hotel>().FindAsync(hotelId)).ReturnsAsync((Hotel?)null);

        await Assert.ThrowsAsync<ArgumentException>(() => _repository.DeleteHotelAsync(hotelId));
    }

    [Fact]
    public async Task GetHotelByIdAsync_ShouldReturnHotel_WhenHotelExists()
    {
        var hotelId = Guid.NewGuid();
        var hotel = new Hotel
        {
            Id = hotelId,
            Name = "Hotel",
            Address = "Address",
        };

        var testList = new List<Hotel> { hotel };

        _mockContext.Setup(m => m.Set<Hotel>())
            .ReturnsDbSet(testList);

        var result = await _repository.GetHotelByIdAsync(hotelId);

        Assert.Equal(hotel, result);
    }

    [Fact]
    public async Task GetHotelByIdAsync_ShouldThrowArgumentException_WhenHotelDoesNotExist()
    {
        var hotelId = Guid.NewGuid();
        var testList = new List<Hotel>();

        _mockContext.Setup(m => m.Set<Hotel>())
            .ReturnsDbSet(testList);

        await Assert.ThrowsAsync<ArgumentException>(() => _repository.GetHotelByIdAsync(hotelId));
    }

    [Fact]
    public async Task GetHotelsAsync_ShouldReturnHotels()
    {
        var hotels = new List<Hotel>
        {
            new() { Id = Guid.NewGuid(), Name = "Hotel 1", Address = "Address 1" },
            new() { Id = Guid.NewGuid(), Name = "Hotel 2", Address = "Address 2" },
        };

        _mockContext.Setup(m => m.Set<Hotel>())
            .ReturnsDbSet(hotels);

        var result = await _repository.GetHotelsAsync();

        Assert.Equal(hotels, result);
    }

    [Fact]
    public async Task UpdateHotelAsync_ShouldUpdateHotel()
    {
        var hotel = new Hotel
        {
            Id = Guid.NewGuid(),
            Name = "Hotel",
            Address = "Address",
        };
        _mockContext.Setup(m => m.Set<Hotel>().Update(hotel)).Verifiable();

        await _repository.UpdateHotelAsync(hotel);

        _mockContext.Verify(m => m.Set<Hotel>().Update(hotel), Times.Once);
    }
}
