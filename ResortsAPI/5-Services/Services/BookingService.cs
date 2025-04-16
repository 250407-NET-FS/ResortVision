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

    public Booking CreateBooking(PostBookingDTO bookingDTO){
        if(bookingDTO.ResortEmail is null || bookingDTO.ResortEmail == ""){
            throw new Exception("Resort Email invalid");
        }
        if(bookingDTO.CustomerEmail is null || bookingDTO.CustomerEmail == ""){
            throw new Exception("Customer Email invalid");
        }
        Resort resort = _resortRepo.Find(bookingDTO.ResortEmail);
        Customer customer = _customerRepo.Find(bookingDTO.CustomerEmail);
        Booking booking = new(customer, resort, resort.Price!);
        decimal price = decimal.Parse(resort.Price!);
        decimal balance = decimal.Parse(customer.Balance);
        if(balance - price < 0){
            throw new Exception("Customer Balance is too low to Book");
        }
        customer.Balance = (balance - price).ToString();
        return AddBooking(booking);
    }

    public Booking AddBooking(Booking booking){
        IBookingService.CheckValidBooking(booking);
        return _bookingRepo.AddBooking(booking);
    }

}