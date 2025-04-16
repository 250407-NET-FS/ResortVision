using ResortsAPI.Models;

namespace ResortsAPI.Repositories;

public interface IBookingRepository
{
    List<Booking> GetAllBookings();

    Booking AddBooking(Booking booking);

    bool SaveBookings(List<Booking> bookings);

    Booking Update(Booking booking);

    List<Booking> Find(string customerEmail);

    List<Booking> FindResort(string resortEmail);
}