using Microsoft.AspNetCore.Mvc;
using InventoryService.Protos;
using InventoryService.Services;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {

        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            try
            {
                var productList = InventoryServiceImpl.Products;
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
