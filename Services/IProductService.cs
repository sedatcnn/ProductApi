using ProductApi.DTOs;
using ProductApi.Models;

namespace ProductApi.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(ProductDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
