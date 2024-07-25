using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Service1.Data;
using Service1.Entities;

namespace Service1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
         private readonly IDbContextFactory _contextFactory;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IDbContextFactory contextFactory, ILogger<ProductController> logger)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("write-mysql")]
        public async Task<IActionResult> WriteMySQL()
        {
            int count = 10000;
            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < count; i++)
            {
                var product = new Product
                {
                    Name = "Product " + i,
                    Description = "Description " + i,
                    Price = i * 10.0,
                    IsAvailable = true
                };
                await _contextFactory.ProductDbContext.Products.AddAsync(product);
            }

            await _contextFactory.ProductDbContext.SaveChangesAsync();
            stopwatch.Stop();
            return Ok(new { TimeElapsed = stopwatch.ElapsedMilliseconds, Count = count });
        }

        [HttpGet("read-mysql")]
        public IActionResult ReadMySQL()
        {
            var stopwatch = Stopwatch.StartNew();
            var products = _contextFactory.ProductDbContext.Products.ToList();
            stopwatch.Stop();
            return Ok(new { TimeElapsed = stopwatch.ElapsedMilliseconds, Count = products.Count });
        }

    }
}