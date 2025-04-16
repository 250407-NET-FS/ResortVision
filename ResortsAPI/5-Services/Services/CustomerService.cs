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

        string shouldBeNull = customer.Memberships.Find(s => s == resort.Email)!;
        if(shouldBeNull is not null){
            throw new Exception("Customer is already Member");
        }
        // add membership

        resort.Members.Add(customer.Email!);
        customer.Memberships.Add(resort.Email!);

        // update repos
        _resortRepo.Update(resort);
        _customerRepo.Update(customer);

        return true;
    }

    public string AddToCustomerBalance(AddToCustomerBalanceDTO balanceDTO){
        // check if amount is valid or email is valid
        if(balanceDTO.Amount is null || balanceDTO.Amount == ""){
            throw new Exception("Invalid Amount");
        }
        if(balanceDTO.CustomerEmail is null || balanceDTO.CustomerEmail == ""){
            throw new Exception("Invalid Email");
        }
        double balance_add = double.Parse(balanceDTO.Amount);
        if(balance_add <= 0.00){
            throw new Exception("Amount must be Positive");
        }
        // get customer
        Customer customer = _customerRepo.Find(balanceDTO.CustomerEmail);
        // add balance
        double balance = double.Parse(customer.Balance);
        balance += balance_add;
        customer.Balance = balance.ToString();
        // save to repo
        _customerRepo.Update(customer);
        return customer.Balance;
    }

}