using Microsoft.AspNetCore.Mvc;
using Redis.Api.Moldes;
using Redis.Api.Repository;
using RedisCache;
using StackExchange.Redis;

namespace Redis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly RedisService _redisService;
        private readonly IDatabase _database;
        public ProductsController(IProductRepository productRepository, IDatabase database)
        {
            _productRepository=productRepository;
           
        }
        [HttpGet]
        public async Task< IActionResult> GetAll()
        {
            return Ok(await _productRepository.GetProductsAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
               return Ok(await _productRepository.GetProductAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create( Product product)
        {
            return Created(string.Empty,await _productRepository.CreateProductAsync(product));
        }
    }
}
