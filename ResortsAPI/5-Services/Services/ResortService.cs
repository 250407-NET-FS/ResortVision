using ResortsAPI.DTOs;
using ResortsAPI.Models;
using ResortsAPI.Repositories;

namespace ResortsAPI.Services;

public class ResortService : IResortService
{
    private readonly IBookingRepository _bookingRepo;

    private readonly ICustomerRepository _customerRepo;

    private readonly IResortRepository _resortRepo;

    public ResortService(
        IBookingRepository bookingRepo,
        ICustomerRepository customerRepo,
        IResortRepository resortRepo
    )
    {
        _bookingRepo = bookingRepo;
        _customerRepo = customerRepo;
        _resortRepo = resortRepo;
    }

    public Resort CreateResort(PostResortDTO resortDTO){
        Resort resort = new(resortDTO.Name!, resortDTO.Email!, resortDTO.Price!);
        return AddResort(resort);
    }

    public Resort AddResort(Resort resort){
        IResortService.CheckValidResort(resort);
        return _resortRepo.AddResort(resort);
    }

    public List<string> GetResortMembers(string email){
        if(email is null || email == ""){
            throw new Exception("Invalid Email");
        }
        Resort resort = _resortRepo.Find(email);
        return resort.Members;
    }

    public List<Booking> GetResortBookings(string email){
        if(email is null || email == ""){
            throw new Exception("Invalid Email");
        }
        Resort resort = _resortRepo.Find(email);
        List<Booking> bookings = _bookingRepo.FindResort(resort.Email!);
        return bookings;
    }

    public string UpdateResortPrice(UpdateResortPriceDTO resortPriceDTO){
        if(resortPriceDTO.ResortEmail is null || resortPriceDTO.ResortEmail == ""){
            throw new Exception("Email Invalid");
        }
        if(resortPriceDTO.Price is null || resortPriceDTO.Price == ""){
            throw new Exception("Price Invalid");
        }
        Resort resort = _resortRepo.Find(resortPriceDTO.ResortEmail);
        resort.Price = resortPriceDTO.Price;
        _resortRepo.Update(resort);
        return resort.Price;
    }

    public List<string> GetResortPerks(string email){
        if(email is null || email == ""){
            throw new Exception("Email is Invalid");
        }
        Resort resort = _resortRepo.Find(email);
        return resort.Perks;
    }

    public string AddResortPerk(ResortPerkDTO resortPerkDTO){
        if(resortPerkDTO.Email is null || resortPerkDTO.Email == ""){
            throw new Exception("Email Invalid");
        }
        if(resortPerkDTO.Perk is null || resortPerkDTO.Perk == ""){
            throw new Exception("Perk Invalid");
        }
        Resort resort = _resortRepo.Find(resortPerkDTO.Email);
        resort.Perks.Add(resortPerkDTO.Perk);
        _resortRepo.Update(resort);
        return resortPerkDTO.Perk;
    }

    public string GetResortPrice(string email){
        if(email is null || email == ""){
            throw new Exception("Email is Invalid");
        }
        Resort resort = _resortRepo.Find(email);
        return resort.Price!;
    }
}