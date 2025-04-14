using ResortsAPI.DTOs;
using ResortsAPI.Models;

namespace ResortsAPI.Repositories;

public interface IResortService
{
    Resort AddResort(Resort resort);

    bool CheckValidResort(Resort resort);
}