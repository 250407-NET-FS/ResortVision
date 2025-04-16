using ResortsAPI.DTOs;
using ResortsAPI.Models;

namespace ResortsAPI.Repositories;

public interface IResortService
{
    Resort CreateResort(PostResortDTO resortDTO);
    Resort AddResort(Resort resort);

    List<string> GetResortMembers(string email);

    List<Booking> GetResortBookings(string email);

    string UpdateResortPrice(UpdateResortPriceDTO resortPriceDTO);

    List<string> GetResortPerks(string email);

    string AddResortPerk(ResortPerkDTO resortPerkDTO);

    string GetResortPrice(string email);

    public static bool CheckValidResort(Resort resort){
        if(resort.Email is null || resort.Email == ""){
            throw new Exception("Invalid Resort Email.");
        }
        if(resort.Name is null || resort.Name == ""){
            throw new Exception("Invalid Resort Name.");
        }
        if(resort.Price is null || resort.Price == ""){
            throw new Exception("Invalid Resort Price.");
        }
        return true;
    }
}