using ResortsAPI.DTOs;
using ResortsAPI.Models;
using ResortsAPI.Repositories;

namespace ResortsAPI.Services;

public class ResortService : IResortService
{
    private readonly IBookingRepository _bookingRepo;

    private readonly ICustomerRepository _customerRepo;

    private readonly IResortRepository _resortRepo;

    public ResortService(
        IBookingRepository bookingRepo,
        ICustomerRepository customerRepo,
        IResortRepository resortRepo
    )
    {
        _bookingRepo = bookingRepo;
        _customerRepo = customerRepo;
        _resortRepo = resortRepo;
    }

    public Resort AddResort(Resort resort){
        CheckValidResort(resort);
        return _resortRepo.AddResort(resort);
    }

    public static bool CheckValidResort(Resort resort){
        if(resort.Email is null || resort.Email == ""){
            throw new Exception("Invalid Resort Email.");
        }
        if(resort.Name is null || resort.Name == ""){
            throw new Exception("Invalid Resort Name.");
        }
        if(resort.Price is null || resort.Price == ""){
            throw new Exception("Invalid Resort Price.");
        }
        return true;
    }

}