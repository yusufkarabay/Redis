using Microsoft.AspNetCore.Mvc;
using Redis.Api.Moldes;
using Redis.Api.Repository;
using Redis.Api.Services;
using RedisCache;
using StackExchange.Redis;

namespace Redis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductRepository productRepository, IDatabase database, IProductService productService)
        {
            _productService=productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productService.GetProductAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            return Created(string.Empty, await _productService.CreateProductAsync(product));
        }
    }
}