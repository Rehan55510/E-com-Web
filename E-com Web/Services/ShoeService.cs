using E_com_Web.Data.Repositories;
using E_com_Web.Models;

namespace E_com_Web.Services;

public class ShoeService : IShoeService
{
    private readonly IRepository<Shoe> _shoeRepository;

    public ShoeService(IRepository<Shoe> shoeRepository)
    {
        _shoeRepository = shoeRepository;
    }

    public async Task<IEnumerable<Shoe>> GetAllShoesAsync()
    {
        return await _shoeRepository.GetAllAsync();
    }

    public async Task<Shoe?> GetShoeByIdAsync(int id)
    {
        return await _shoeRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Shoe>> GetShoesByCategoryAsync(string category)
    {
        var allShoes = await _shoeRepository.GetAllAsync();
        return allShoes.Where(s => s.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<IEnumerable<Shoe>> SearchShoesAsync(string searchTerm)
    {
        var allShoes = await _shoeRepository.GetAllAsync();
        return allShoes.Where(s =>
            s.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            s.Brand.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            s.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
        );
    }

    public async Task<IEnumerable<string>> GetCategoriesAsync()
    {
        var allShoes = await _shoeRepository.GetAllAsync();
        return allShoes.Select(s => s.Category).Distinct();
    }

    public async Task AddShoeAsync(Shoe shoe)
    {
        await _shoeRepository.AddAsync(shoe);
    }

    public async Task UpdateShoeAsync(Shoe shoe)
    {
        await _shoeRepository.UpdateAsync(shoe);
    }

    public async Task DeleteShoeAsync(int id)
    {
        await _shoeRepository.DeleteAsync(id);
    }
}
