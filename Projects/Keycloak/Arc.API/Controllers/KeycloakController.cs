using Microsoft.AspNetCore.Mvc;
using Arc.Application.Interfaces;
using System.Threading.Tasks;
using Arc.Domain.Entities;

namespace Arc.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeycloakController : ControllerBase
    {
        private readonly IKeycloakService _keycloakService;

        public KeycloakController(IKeycloakService keycloakService)
        {
            _keycloakService = keycloakService;
        }

        [HttpGet("admin-token")]
        public async Task<IActionResult> GetAdminToken()
        {
            var token = await _keycloakService.GetAdminTokenAsync();
            return Ok(new { Token = token });
        }

        [HttpPost("create-realm")]
        public async Task<IActionResult> CreateRealm([FromBody] string realmName)
        {
            await _keycloakService.CreateRealmAsync(realmName);
            return Ok("Realm created successfully");
        }

        [HttpPost("create-client")]
        public async Task<IActionResult> CreateClient(string ClientName)
        {
            await _keycloakService.CreateClientAsync(ClientName);
            return Ok("Client created successfully");
        }

        [HttpPost("create-role-for-client")]
        public async Task<IActionResult> CreateRole(string clientName, string roleName)
        {
            await _keycloakService.CreateRoleForClientAsync(clientName, roleName);
            return Ok("Role created successfully");
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            await _keycloakService.CreateRoleAsync(roleName);
            return Ok("Role created successfully");
        }

        [HttpPost("create-group")]
        public async Task<IActionResult> CreateGroup(string groupName)
        {
            await _keycloakService.CreateGroupAsync(groupName);
            return Ok("Group created successfully");
        }

        [HttpPost("assign-role-to-group")]
        public async Task<IActionResult> AssignRoleToGroup([FromQuery] string groupId, [FromQuery] string roleName)
        {
            await _keycloakService.AssignRoleToGroupAsync(groupId, roleName);
            return Ok("Role assigned to group successfully");
        }

        [HttpPost("assign-roles-to-group-by-client")]
        public async Task<IActionResult> AssignRolesToGroupByClient(string groupName, string clientId, [FromBody] string[] roleNames)
        {
            await _keycloakService.AssignRolesToGroupByClientAsync(groupName, clientId, roleNames);
            return Ok("Roles assigned to group successfully");
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] User user, [FromQuery] string groupId)
        {
            await _keycloakService.CreateUserAsync(user, groupId);
            return Ok("User created successfully");
        }
    }
}
