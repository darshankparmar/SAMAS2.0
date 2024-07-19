using Microsoft.AspNetCore.Mvc;

namespace Service1.API.Controllers
{

    // [ApiController] Attribute: This attribute indicates that the controller responds to web API requests. 
    // It provides several built-in behaviors such as automatic model validation.
    [ApiController]
    // [Route("api/[controller]")] Attribute: This attribute defines the routing template for the controller. 
    // The [controller] token is replaced with the name of the controller (minus the "Controller" suffix), resulting in a route like api/customer for the CustomerController.
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
       // The ApiControllerBase class serves as a base class for all API controllers in your application. 
       // It is an abstract class that inherits from ControllerBase, providing shared functionality or configuration for derived controllers.
    }
}