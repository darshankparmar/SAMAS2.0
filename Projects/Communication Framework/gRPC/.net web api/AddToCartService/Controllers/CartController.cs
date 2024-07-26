using AddToCartService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AddToCartService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly GrpcServiceClient _grpcClient;

        public CartController(GrpcServiceClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(string productId, int quantity)
        {
            var productList = await _grpcClient.GetProductListAsync();

            var product = productList.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null || product.Quantity < quantity)
            {
                return BadRequest("Product not available or insufficient quantity.");
            }

            // Here you would typically add the product to the cart
            // For simplicity, we'll just return a success message
            return Ok("Product added to cart successfully.");
        }
    }
}
