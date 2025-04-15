using ResortsAPI.Models;

namespace ResortsAPI.Repositories;

public interface IResortRepository
{
    List<Resort> GetAllResorts();

    Resort AddResort(Resort resort);

    bool SaveResorts(List<Resort> resorts);

    Resort Update(Resort resort);

    Resort Find(string email);
}