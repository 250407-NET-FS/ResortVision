namespace  ResortsAPI.Models;
public class Resort(string Name, string Email, string Price, List<string> Perks){

    public Guid ResortId { get; set; } = Guid.NewGuid();
    
    public string? Email { get; set; } = Email;

    public string? Name { get; set; } = Name;

    public List<Customer> Members { get; set; } = [];

    public string? Price { get; set; } = Price;

    public List<string>? Perks { get; set; } = Perks;

    public List<Booking> Bookings { get; set; } = [];
}
