using ProductApi.DTOs;
using ProductApi.Models;
using ProductApi.Repositories;

namespace ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Product>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Product?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<Product> CreateAsync(ProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock
            };
            await _repo.AddAsync(product);
            await _repo.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return false;
            await Task.Run(() => _repo.DeleteAsync(product));
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
