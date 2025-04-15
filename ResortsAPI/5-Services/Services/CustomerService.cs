using ResortsAPI.DTOs;
using ResortsAPI.Models;
using ResortsAPI.Repositories;

namespace ResortsAPI.Services;

public class CustomerService : ICustomerService
{
    private readonly IBookingRepository _bookingRepo;

    private readonly ICustomerRepository _customerRepo;

    private readonly IResortRepository _resortRepo;

    public CustomerService(
        IBookingRepository bookingRepo,
        ICustomerRepository customerRepo,
        IResortRepository resortRepo
    )
    {
        _bookingRepo = bookingRepo;
        _customerRepo = customerRepo;
        _resortRepo = resortRepo;
    }

    public Customer AddCustomer(Customer customer){
        CheckValidCustomer(customer);
        _customerRepo.AddCustomer(customer);
        return customer;
    }

    public static bool CheckValidCustomer(Customer customer){
        if(customer.Email is null || customer.Email == ""){
            throw new Exception("Invalid Customer Email.");
        }
        if(customer.FName is null || customer.FName == ""){
            throw new Exception("Invalid Customer First Name.");
        }
        if(customer.LName is null || customer.LName == ""){
            throw new Exception("Invalid Customer Last Name.");
        }
        return true;
    }

}