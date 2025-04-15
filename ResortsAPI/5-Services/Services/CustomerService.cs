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
        ICustomerService.CheckValidCustomer(customer);
        return _customerRepo.AddCustomer(customer);
    }

    public Customer CreateCustomer(PostCustomerDTO customerDTO){
        Customer customer = new(customerDTO.FName!, customerDTO.LName!, customerDTO.Email!);
        return AddCustomer(customer);
    }

}