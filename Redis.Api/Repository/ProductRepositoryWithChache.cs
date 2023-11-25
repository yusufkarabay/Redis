using Redis.Api.Moldes;
using RedisCache;
using StackExchange.Redis;
using System.Text.Json;

namespace Redis.Api.Repository
{
    public class ProductRepositoryWithChache : IProductRepository
    {
        private const string key = "products";
        private readonly IProductRepository _productRepository;
        private readonly RedisService _redisService;
        private readonly IDatabase _database;

        public ProductRepositoryWithChache(IProductRepository productRepository, RedisService redisService)
        {
            _productRepository=productRepository;
            _redisService=redisService;
            _database=_redisService.GetDb(1);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            var newProduct = _productRepository.CreateProductAsync(product);
            if (await _database.KeyExistsAsync(key))
            {
                await _database.HashSetAsync(key, product.Id.ToString(), JsonSerializer.Serialize(product));
            }
            return await newProduct;
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            if (await _database.KeyExistsAsync(key))
            {
                var product = await _database.HashGetAsync(key, id.ToString());
                return product.HasValue ? JsonSerializer.Deserialize<Product>(product) : null;
            }
            var getAllProducts = await GetProductsFromCacheAsync();
            return getAllProducts.FirstOrDefault(x => x.Id==id.ToString());
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            if (!await _database.KeyExistsAsync(key))
                return await GetProductsFromCacheAsync();
            var products = new List<Product>();
            var cacheProducts = await _database.HashGetAllAsync(key);
            foreach (var item in cacheProducts.ToList())
            {
                var product = JsonSerializer.Deserialize<Product>(item.Value);
                products.Add(product);
            }
            return products;
        }

        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        private async Task<List<Product>> GetProductsFromCacheAsync()
        {
            var productsGetAll = await _productRepository.GetProductsAsync();
            productsGetAll.ForEach(x =>
            {
                _database.HashSetAsync(key, x.Id.ToString(), JsonSerializer.Serialize(x));
            });
            return productsGetAll;
        }
    }
}