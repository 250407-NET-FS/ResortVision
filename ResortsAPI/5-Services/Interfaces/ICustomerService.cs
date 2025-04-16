using ResortsAPI.DTOs;
using ResortsAPI.Models;

namespace ResortsAPI.Repositories;

public interface ICustomerService
{
    Customer CreateCustomer(PostCustomerDTO customerDTO);
    Customer AddCustomer(Customer customer);

    string AddToCustomerBalance(AddToCustomerBalanceDTO balanceDTO);

    bool AddMember(ResortMemberDTO memberDTO);

    List<Booking> GetCustomerBooking(string email);

    List<string> GetCustomerMemberships(string email);

    bool DeleteMembership(ResortMemberDTO resortMemberDTO);

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