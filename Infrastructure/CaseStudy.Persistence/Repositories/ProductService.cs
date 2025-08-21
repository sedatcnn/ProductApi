using CaseStudy.Domian.Entites;
using CaseStudy.Domian.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CaseStudy.Persistence.Repositories
{
    public class ProductService : IProductService
    {
        private readonly IDistributedCache _cache;
        private readonly IProductRepository<Product> _repository; // <-- Düzelttik

        public ProductService(IDistributedCache cache, IProductRepository<Product> repository)
        {
            _cache = cache;
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var cacheKey = "ProductApi_products_all";
            var cached = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cached))
                return JsonSerializer.Deserialize<IEnumerable<Product>>(cached)!;

            var products = await _repository.GetAllAsync();
            Console.WriteLine($"DB’den çekilen ürün sayısı: {products.Count}");

            await _cache.SetStringAsync(cacheKey,
                JsonSerializer.Serialize(products),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            Console.WriteLine("Redis cache’e yazıldı.");

            return products;
        }

        public async Task AddAsync(Product product)
        {
            await _repository.CreatAsync(product); // Repository’de "CreatAsync" yazmışsın dikkat 🚨
            await _cache.RemoveAsync("ProductApi_products_all"); // Cache invalidation
        }
    }
}
