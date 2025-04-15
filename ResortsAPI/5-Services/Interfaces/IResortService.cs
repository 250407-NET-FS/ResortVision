using ResortsAPI.DTOs;
using ResortsAPI.Models;

namespace ResortsAPI.Repositories;

public interface IResortService
{
    Resort AddResort(Resort resort);

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