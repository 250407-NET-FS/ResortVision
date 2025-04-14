using System.Text.Json;
using ResortsAPI.Models;

namespace Library.Repositories;

public class JsonResortRepository : IResortRepository
{
    private readonly string _filePath;

    public JsonResortRepository(){
        _filePath = "./4-Data-Files/resorts.json";
    }

    public List<Resort> GetAllResorts()
    {
        try
        {
            if (!File.Exists(_filePath))
                return [];

            using var stream = File.OpenRead(_filePath);

            return JsonSerializer.Deserialize<List<Resort>>(stream) ?? [];
        }
        catch
        {
            throw new Exception("Failed to retrieve resorts");
        }
    }

    public Resort AddResort(Resort resort)
    {
        List<Resort> resorts = GetAllResorts();
        if (resorts.Any(r => r.Email == resort.Email))
            throw new Exception("Resort with same Email already exists");
        resorts.Add(resort);
        SaveResorts(resorts);
        return resort;
    }

    public bool SaveResorts(List<Resort> resorts){
        using var stream = File.Create(_filePath); //Creating the file
        JsonSerializer.Serialize(stream, resorts);
        return true;
    }

    public Resort Update(Resort resort){
        List<Resort> resorts = GetAllResorts();

        int index = resorts.FindIndex(r => r.Email == resort.Email);

        if(index == -1){
            throw new Exception("Resort not found.");
        }

        resorts[index] = resort;
        SaveResorts(resorts);
        return resort;
    }
}