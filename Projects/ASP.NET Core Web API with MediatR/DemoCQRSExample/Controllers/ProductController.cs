using Microsoft.AspNetCore.Mvc;
using DemoCQRSExample.Commands;
using DemoCQRSExample.Queries;
using MediatR;

namespace DemoCQRSExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var response = await _mediator.Send(new GetProductsQuery());
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }        
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            var productId = await _mediator.Send(command);
            return Ok(productId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var query = new GetProductQuery { Id = id };
            var product = await _mediator.Send(query);
            return Ok(product);
        }
    }
}