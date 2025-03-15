// <copyright file="BookingServiceTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Core.Tests;

using AutoMapper;
using BookingSystem.Core.Models.Booking;
using BookingSystem.Core.Models.Room;
using BookingSystem.Core.Services;
using BookingSystem.Data.Models;
using BookingSystem.Data.UnitOfWork.Abstractions;
using Moq;

public class BookingServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly BookingService _bookingService;

    public BookingServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _bookingService = new BookingService(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task AddBookingAsync_ShouldAddBooking()
    {
        // Arrange
        var createBookingDto = new CreateBookingDto
        {
            TenantName = "John Doe",
            TenantPassportNumber = "123456789",
            TenantPhoneNumber = "555-1234",
        };
        var booking = new Booking
        {
            TenantName = "John Doe",
            TenantPassportNumber = "123456789",
            TenantPhoneNumber = "555-1234",
        };
        _mapperMock.Setup(m => m.Map<Booking>(createBookingDto)).Returns(booking);
        _unitOfWorkMock.Setup(u => u.Bookings.AddAsync(booking)).Verifiable();
        _unitOfWorkMock.Setup(u => u.Rooms.GetRoomByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Room());

        // Act
        await _bookingService.AddBookingAsync(createBookingDto);

        // Assert
        _unitOfWorkMock.Verify(u => u.Bookings.AddAsync(booking), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteBookingAsync_ShouldDeleteBooking()
    {
        // Arrange
        var bookingId = Guid.NewGuid();
        _unitOfWorkMock.Setup(u => u.Bookings.DeleteAsync(bookingId)).Verifiable();

        // Act
        await _bookingService.DeleteBookingAsync(bookingId);

        // Assert
        _unitOfWorkMock.Verify(u => u.Bookings.DeleteAsync(bookingId), Times.Once);
    }

    [Fact]
    public async Task GetBookingAsync_ShouldReturnBooking()
    {
        // Arrange
        var bookingId = Guid.NewGuid();
        var booking = new Booking
        {
            Id = bookingId,
            TenantName = "John Doe",
            TenantPassportNumber = "123456789",
            TenantPhoneNumber = "555-1234",
        };
        var fullBookingDto = new FullBookingDto
        {
            Id = bookingId,
            TenantName = "John Doe",
            TenantPassportNumber = "123456789",
            TenantPhoneNumber = "555-1234",
            Room = new BriefRoomDto(),
        };
        _unitOfWorkMock.Setup(u => u.Bookings.GetByIdAsync(bookingId)).ReturnsAsync(booking);
        _mapperMock.Setup(m => m.Map<FullBookingDto>(booking)).Returns(fullBookingDto);

        // Act
        var result = await _bookingService.GetBookingAsync(bookingId);

        // Assert
        Assert.Equal(fullBookingDto, result);
    }

    [Fact]
    public async Task GetBookingsAsync_ShouldReturnBookings()
    {
        // Arrange
        var bookings = new List<Booking>
        {
            new()
            {
                Id = Guid.NewGuid(),
                TenantName = "John Doe",
                TenantPassportNumber = "123456789",
                TenantPhoneNumber = "555-1234",
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantName = "Jane Smith",
                TenantPassportNumber = "987654321",
                TenantPhoneNumber = "555-5678",
            },
        };
        var briefBookingDtos = new List<BriefBookingDto>
        {
            new()
            {
                Id = bookings[0].Id,
                TenantName = "John Doe",
                TenantPassportNumber = "123456789",
                TenantPhoneNumber = "555-1234",
            },
            new()
            {
                Id = bookings[1].Id,
                TenantName = "Jane Smith",
                TenantPassportNumber = "987654321",
                TenantPhoneNumber = "555-5678",
            },
        };
        _unitOfWorkMock.Setup(u => u.Bookings.GetAllAsync()).ReturnsAsync(bookings);
        _mapperMock.Setup(m => m.Map<IEnumerable<BriefBookingDto>>(bookings)).Returns(briefBookingDtos);

        // Act
        var result = await _bookingService.GetBookingsAsync();

        // Assert
        Assert.Equal(briefBookingDtos, result);
    }

    [Fact]
    public async Task UpdateBookingAsync_ShouldUpdateBooking()
    {
        // Arrange
        var bookingId = Guid.NewGuid();
        var updateBookingDto = new UpdateBookingDto
        {
            Id = bookingId,
            TenantName = "John Doe",
            TenantPassportNumber = "123456789",
            TenantPhoneNumber = "555-1234",
        };
        var booking = new Booking
        {
            Id = bookingId,
            TenantName = "John Doe",
            TenantPassportNumber = "123456789",
            TenantPhoneNumber = "555-1234",
        };
        _unitOfWorkMock.Setup(u => u.Bookings.GetByIdAsync(bookingId)).ReturnsAsync(booking);
        _unitOfWorkMock.Setup(u => u.Rooms.GetRoomByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Room());

        // Act
        await _bookingService.UpdateBookingAsync(bookingId, updateBookingDto);

        // Assert
        _mapperMock.Verify(m => m.Map(updateBookingDto, booking), Times.Once);
        _unitOfWorkMock.Verify(u => u.Bookings.UpdateAsync(booking), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateBookingAsync_ShouldThrowArgumentException_WhenIdsDoNotMatch()
    {
        // Arrange
        var bookingId = Guid.NewGuid();
        var updateBookingDto = new UpdateBookingDto
        {
            Id = Guid.NewGuid(),
            TenantName = "John Doe",
            TenantPassportNumber = "123456789",
            TenantPhoneNumber = "555-1234",
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _bookingService.UpdateBookingAsync(bookingId, updateBookingDto));
    }
}