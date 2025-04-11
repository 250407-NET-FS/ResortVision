namespace ResortsAPI.Models;

public class Booking(Customer Customer, Resort Resort, string Cost)
{
    public Guid BookingId { get; set; } = Guid.NewGuid();
    
    public Customer? Customer { get; set; } = Customer;

    public Resort? Resort { get; set; } = Resort;

    public string? Cost { get; set; } = Cost;

    public DateTime Date { get; set; } = DateTime.Now;
}