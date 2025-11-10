using E_com_Web.Models;

namespace E_com_Web.Services;

public interface IShoeService
{
    List<Shoe> GetAllShoes();
    Shoe? GetShoeById(int id);
    List<Shoe> GetShoesByCategory(string category);
    List<Shoe> SearchShoes(string searchTerm);
    List<string> GetCategories();
}

