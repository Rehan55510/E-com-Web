using E_com_Web.Models;

namespace E_com_Web.Services;

public interface IShoeService
{
    Task<IEnumerable<Shoe>> GetAllShoesAsync();
    Task<Shoe?> GetShoeByIdAsync(int id);
    Task<IEnumerable<Shoe>> GetShoesByCategoryAsync(string category);
    Task<IEnumerable<Shoe>> SearchShoesAsync(string searchTerm);
    Task<IEnumerable<string>> GetCategoriesAsync();
    
    // CRUD operations
    Task AddShoeAsync(Shoe shoe);
    Task UpdateShoeAsync(Shoe shoe);
    Task DeleteShoeAsync(int id);
}

