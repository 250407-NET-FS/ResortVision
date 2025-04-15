using ResortsAPI.Models;

namespace ResortsAPI.Repositories;

public interface ICustomerRepository
{
    List<Customer> GetAllCustomers();

    Customer AddCustomer(Customer customer);

    bool SaveCustomers(List<Customer> customers);

    Customer Update(Customer customer);

    Customer Find(string email);
}