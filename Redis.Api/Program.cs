using Microsoft.EntityFrameworkCore;
using Redis.Api.Moldes;
using Redis.Api.Repository;
using Redis.Api.Services;
using RedisCache;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("mydatabase"));
builder.Services.AddScoped<IProductRepository >(options=>
{
    var appDbContext = options.GetRequiredService<AppDbContext>();
    var productRepository = new ProductRepository(appDbContext);
   
    return new ProductRepositoryWithChache(productRepository, options.GetRequiredService<RedisService>());

});
builder.Services.AddSingleton<RedisService>(options =>
{
   return new RedisService(builder.Configuration["CacheOptions:Url"]);
});

builder.Services.AddSingleton<IDatabase>(options=>
{
    var redisService = options.GetRequiredService<RedisService>();
    return redisService.GetDb(0);
});
builder.Services.AddScoped<IProductService, ProductService>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
