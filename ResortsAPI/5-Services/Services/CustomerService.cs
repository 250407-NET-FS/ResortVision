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

    public bool AddMember(ResortMemberDTO memberDTO){
        // get customer and resort and check if input is valid
        if(memberDTO.CustomerEmail is null || memberDTO.CustomerEmail == ""){
            throw new Exception("Customer Email invalid");
        }
        if(memberDTO.ResortEmail is null || memberDTO.ResortEmail == ""){
            throw new Exception("Resort Email invalid");
        }
        Customer customer = _customerRepo.Find(memberDTO.CustomerEmail!);
        Resort resort = _resortRepo.Find(memberDTO.ResortEmail);
        
        // check if customer is already member

        Resort shouldBeNull = customer.Memberships.Find(r => r.Email == resort.Email)!;
        if(shouldBeNull is not null){
            throw new Exception("Customer is already Member");
        }
        // add membership

        resort.Members.Add(customer);
        customer.Memberships.Add(resort);

        // update repos
        _resortRepo.Update(resort);
        _customerRepo.Update(customer);

        return true;
    }

}