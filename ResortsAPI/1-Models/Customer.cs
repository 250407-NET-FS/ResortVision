namespace ResortsAPI.Models;

public class Customer(string FName, string LName, string Email)
{
    public Guid CustomerId { get; set; } = Guid.NewGuid();

    public string? FName { get; set; } = FName;

    public string? LName { get; set; } = LName;

    public string? Email { get; set; } = Email;

    public string Balance { get; set; } = "0.00";

    public List<Booking> Bookings { get; set; } = [];
    
    public List<Resort> Memberships { get; set; } = [];
}