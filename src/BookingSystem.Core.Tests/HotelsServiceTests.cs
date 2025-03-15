// <copyright file="HotelsServiceTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Core.Tests;

using AutoMapper;
using BookingSystem.Core.Models.Hotel;
using BookingSystem.Core.Models.Room;
using BookingSystem.Core.Services;
using BookingSystem.Data.Models;
using BookingSystem.Data.UnitOfWork.Abstractions;
using Moq;

public class HotelsServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly HotelsService _hotelsService;

    public HotelsServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _hotelsService = new HotelsService(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task AddHotelAsync_ShouldAddHotel()
    {
        // Arrange
        var createHotelDto = new CreateHotelDto
        {
            Name = "Hotel California",
            Address = "42 Sunset Blvd",
        };
        var hotel = new Hotel
        {
            Name = "Hotel California",
            Address = "42 Sunset Blvd",
        };
        _mapperMock.Setup(m => m.Map<Hotel>(createHotelDto)).Returns(hotel);
        _unitOfWorkMock.Setup(u => u.Hotels.AddHotelAsync(hotel)).Verifiable();

        // Act
        await _hotelsService.AddHotelAsync(createHotelDto);

        // Assert
        _unitOfWorkMock.Verify(u => u.Hotels.AddHotelAsync(hotel), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteHotelAsync_ShouldDeleteHotel()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        _unitOfWorkMock.Setup(u => u.Hotels.DeleteHotelAsync(hotelId)).Verifiable();

        // Act
        await _hotelsService.DeleteHotelAsync(hotelId);

        // Assert
        _unitOfWorkMock.Verify(u => u.Hotels.DeleteHotelAsync(hotelId), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task GetHotelAsync_ShouldReturnHotel()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        var hotel = new Hotel
        {
            Id = hotelId,
            Name = "Hotel California",
            Address = "42 Sunset Blvd",
        };
        var fullHotelDto = new FullHotelDto
        {
            Id = hotelId,
            Name = "Hotel California",
            Address = "42 Sunset Blvd",
            Rooms = [],
        };
        _unitOfWorkMock.Setup(u => u.Hotels.GetHotelByIdAsync(hotelId)).ReturnsAsync(hotel);
        _mapperMock.Setup(m => m.Map<FullHotelDto>(hotel)).Returns(fullHotelDto);

        // Act
        var result = await _hotelsService.GetHotelAsync(hotelId);

        // Assert
        Assert.Equal(fullHotelDto, result);
    }

    [Fact]
    public async Task GetHotelsAsync_ShouldReturnHotels()
    {
        // Arrange
        var hotels = new List<Hotel>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel California",
                Address = "42 Sunset Blvd",
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel Transylvania",
                Address = "13 Spooky St",
            },
        };
        var briefHotelDtos = new List<BriefHotelDto>
        {
            new()
            {
                Id = hotels[0].Id,
                Name = "Hotel California",
                Address = "42 Sunset Blvd",
            },
            new()
            {
                Id = hotels[1].Id,
                Name = "Hotel Transylvania",
                Address = "13 Spooky St",
            },
        };
        _unitOfWorkMock.Setup(u => u.Hotels.GetHotelsAsync()).ReturnsAsync(hotels);
        _mapperMock.Setup(m => m.Map<IEnumerable<BriefHotelDto>>(hotels)).Returns(briefHotelDtos);

        // Act
        var result = await _hotelsService.GetHotelsAsync();

        // Assert
        Assert.Equal(briefHotelDtos, result);
    }

    [Fact]
    public async Task UpdateHotelAsync_ShouldUpdateHotel()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        var updateHotelDto = new UpdateHotelDto
        {
            Id = hotelId,
            Name = "Hotel California",
            Address = "42 Sunset Blvd",
        };
        var hotel = new Hotel
        {
            Id = hotelId,
            Name = "Hotel California",
            Address = "42 Sunset Blvd",
        };
        _unitOfWorkMock.Setup(u => u.Hotels.GetHotelByIdAsync(hotelId)).ReturnsAsync(hotel);

        // Act
        await _hotelsService.UpdateHotelAsync(hotelId, updateHotelDto);

        // Assert
        _mapperMock.Verify(m => m.Map(updateHotelDto, hotel), Times.Once);
        _unitOfWorkMock.Verify(u => u.Hotels.UpdateHotelAsync(hotel), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateHotelAsync_ShouldThrowArgumentException_WhenIdsDoNotMatch()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        var updateHotelDto = new UpdateHotelDto
        {
            Id = Guid.NewGuid(),
            Name = "Hotel California",
            Address = "42 Sunset Blvd",
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _hotelsService.UpdateHotelAsync(hotelId, updateHotelDto));
    }
}