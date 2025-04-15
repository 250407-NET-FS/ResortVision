using ResortsAPI.DTOs;
using ResortsAPI.Models;
using ResortsAPI.Repositories;
using ResortsAPI.Services;

namespace ResortsAPI.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepo;

    private readonly ICustomerRepository _customerRepo;

    private readonly IResortRepository _resortRepo;

    public BookingService(
        IBookingRepository bookingRepo,
        ICustomerRepository customerRepo,
        IResortRepository resortRepo
    )
    {
        _bookingRepo = bookingRepo;
        _customerRepo = customerRepo;
        _resortRepo = resortRepo;
    }

    public Booking AddBooking(Booking booking){
        CheckValidBooking(booking);
        return _bookingRepo.AddBooking(booking);
    }

    public static bool CheckValidBooking(Booking booking){
        if(booking.Customer is null || !CustomerService.CheckValidCustomer(booking.Customer)){
            throw new Exception("Invalid Booking Customer.");
        }
        if(booking.Resort is null || !ResortService.CheckValidResort(booking.Resort)){
            throw new Exception("Invalid Booking Resort.");
        }
        if(booking.Cost is null || booking.Cost == ""){
            throw new Exception("Invalid Booking Cost.");
        }
        return true;
    }

}