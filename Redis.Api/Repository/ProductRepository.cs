using Redis.Api.Moldes;

namespace Redis.Api.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context=context;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return  await _context.Products.FindAsync(id);
        }

        public Task<List<Product>> GetProductsAsync()
        { 
            return Task.FromResult<List<Product>>(_context.Products.ToList());
        }

        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
