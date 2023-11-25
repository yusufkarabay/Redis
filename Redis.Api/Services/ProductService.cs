using Redis.Api.Moldes;
using Redis.Api.Repository;

namespace Redis.Api.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(int id);
        Task<Product> CreateProductAsync(Product product);
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository=productRepository;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
          return  await _productRepository.CreateProductAsync(product);
             
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return  await _productRepository.GetProductAsync(id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _productRepository.GetProductsAsync();
        }
    }
}
