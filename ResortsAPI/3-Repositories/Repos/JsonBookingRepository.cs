using System.Text.Json;
using ResortsAPI.Models;

namespace Library.Repositories;

public class JsonBookingRepository : IBookingRepository
{
    private readonly string _filePath;
    public JsonBookingRepository(){
        _filePath = "./4-Data-Files/bookings.json";
    }

    public List<Booking> GetAllBookings()
    {
        try
        {
            if (!File.Exists(_filePath))
                return [];

            using var stream = File.OpenRead(_filePath);

            return JsonSerializer.Deserialize<List<Booking>>(stream) ?? [];
        }
        catch
        {
            throw new Exception("Failed to retrieve bookings");
        }
    }

    public Booking AddBooking(Booking booking)
    {
        List<Booking> bookings = GetAllBookings();
        if (bookings.Any(b => b.BookingId == booking.BookingId))
            throw new Exception("Booking with same ID already exists");
        bookings.Add(booking);
        SaveBooks(bookings);
        return booking;
    }

    public bool SaveBooks(List<Booking> bookings){
        using var stream = File.Create(_filePath); //Creating the file
        JsonSerializer.Serialize(stream, bookings);
        return true;
    }

    public Booking Update(Booking booking){
        List<Booking> bookings = GetAllBookings();

        int index = bookings.FindIndex(b => b.BookingId == booking.BookingId);

        if(index == -1){
            throw new Exception("Booking not found.");
        }

        bookings[index] = booking;
        SaveBooks(bookings);
        return booking;
    }

}