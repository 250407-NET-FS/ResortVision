using System.Text.Json;
using ResortsAPI.Models;

namespace ResortsAPI.Repositories;

public class JsonCustomerRepository : ICustomerRepository
{
    private readonly string _filePath;

    public JsonCustomerRepository(){
        _filePath = "./4-Data-Files/customers.json";
    }

    public List<Customer> GetAllCustomers()
    {
        try
        {
            if (!File.Exists(_filePath))
                return [];

            using var stream = File.OpenRead(_filePath);

            return JsonSerializer.Deserialize<List<Customer>>(stream) ?? [];
        }
        catch
        {
            throw new Exception("Failed to retrieve customers");
        }
    }

    public Customer AddCustomer(Customer customer)
    {
        List<Customer> customers = GetAllCustomers();
        if (customers.Any(c => c.Email == customer.Email))
            throw new Exception("Customer with same Email already exists");
        customers.Add(customer);
        SaveCustomers(customers);
        return customer;
    }

    public bool SaveCustomers(List<Customer> customers){
        using var stream = File.Create(_filePath); //Creating the file
        JsonSerializer.Serialize(stream, customers);
        return true;
    }

    public Customer Update(Customer customer){
        List<Customer> customers = GetAllCustomers();

        int index = customers.FindIndex(c => c.Email == customer.Email);

        if(index == -1){
            throw new Exception("Customer not found.");
        }

        customers[index] = customer;
        SaveCustomers(customers);
        return customer;
    }

    public Customer Find(string email){
        List<Customer> customers = GetAllCustomers();

        int index = customers.FindIndex(c => c.Email == email);

        if(index == -1){
            throw new Exception("Customer not found.");
        }
        return customers[index];
    }
}