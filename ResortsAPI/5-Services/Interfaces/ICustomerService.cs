using ResortsAPI.DTOs;
using ResortsAPI.Models;

namespace ResortsAPI.Repositories;

public interface ICustomerService
{
    Customer AddCustomer(Customer customer);

    bool CheckValidCustomer(Customer customer);
}