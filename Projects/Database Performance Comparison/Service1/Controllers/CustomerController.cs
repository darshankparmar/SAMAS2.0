using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Service1.Entities;
using Service1.Data;

namespace Service1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IDbContextFactory _contextFactory;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IDbContextFactory contextFactory, ILogger<CustomerController> logger)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("write-postgresql")]
        public async Task<IActionResult> WritePostgreSQL()
        {
            int count = 10000;
            var stopwatch = Stopwatch.StartNew();
            
            for (int i = 0; i < count; i++)
            {
                var customer = new Customer
                {
                    Name = "Name " + i,
                    Address = "Address " + i,
                    Email = "email" + i + "@example.com",
                    MobileNo = "1234567890"
                };
                await _contextFactory.CustomerDbContext.Customers.AddAsync(customer);
            }
            
            await _contextFactory.CustomerDbContext.SaveChangesAsync();
            stopwatch.Stop();
            return Ok(new { TimeElapsed = stopwatch.ElapsedMilliseconds, Count = count });
        }

        [HttpGet("read-postgresql")]
        public IActionResult ReadPostgreSQL()
        {
            var stopwatch = Stopwatch.StartNew();
            var customers = _contextFactory.CustomerDbContext.Customers.ToList();
            stopwatch.Stop();
            return Ok(new { TimeElapsed = stopwatch.ElapsedMilliseconds, Count = customers.Count });
        }

    }
}