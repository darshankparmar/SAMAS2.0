using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arc.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class HomeController
    {
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IEnumerable<string> GetForAdmin()
        {
            // Logic for admin
            return new List<string>(){"Hello,","Hii","Hola Amigo"};
        }

        [HttpGet("user")]
        [Authorize(Roles = "moderator")]
        public IEnumerable<string> GetForModerator()
        {
            // Logic for user
            return new List<string>(){"Hello,","Hii","Hola Amigo"};
        }

    }

}