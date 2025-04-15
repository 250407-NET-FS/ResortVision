using ResortsAPI.DTOs;
using ResortsAPI.Models;

namespace ResortsAPI.Repositories;

public interface IBookingService
{
    Booking CreateBooking(PostBookingDTO bookingDTO);
    Booking AddBooking(Booking booking);
    public static bool CheckValidBooking(Booking booking){
        if(booking.Customer is null || !ICustomerService.CheckValidCustomer(booking.Customer)){
            throw new Exception("Invalid Booking Customer.");
        }
        if(booking.Resort is null || !IResortService.CheckValidResort(booking.Resort)){
            throw new Exception("Invalid Booking Resort.");
        }
        if(booking.Cost is null || booking.Cost == ""){
            throw new Exception("Invalid Booking Cost.");
        }
        return true;
    }
}