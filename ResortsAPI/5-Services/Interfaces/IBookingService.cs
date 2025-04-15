using ResortsAPI.DTOs;
using ResortsAPI.Models;

namespace ResortsAPI.Repositories;

public interface IBookingService
{
    Booking AddBooking(Booking booking);
    bool CheckValidBooking(Booking booking);
}