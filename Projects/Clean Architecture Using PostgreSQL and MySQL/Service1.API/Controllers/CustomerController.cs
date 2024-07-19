using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service1.Application.DTOs;
using Service1.Application.IServices;
using Service1.Domain.Entities;

namespace Service1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger, IMapper mapper)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomerDetails()
        {
            try
            {
                var customers = await _customerService.GetAllCustomersAsync();
                var customerDtos = _mapper.Map<List<CustomerDto>>(customers);
                return Ok(customerDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all customer details.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }
                var customerDto = _mapper.Map<CustomerDto>(customer);
                return Ok(customerDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting customer details for ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> RegisterCustomer([FromBody] CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                string message = await _customerService.AddCustomerAsync(customer);
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the customer.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateCustomer(int id, [FromBody] CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerDto.Id)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                string message = await _customerService.UpdateCustomerAsync(customer);
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the customer with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteCustomer(int id)
        {
            try
            {
                string message = await _customerService.DeleteCustomerAsync(id);
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the customer with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}