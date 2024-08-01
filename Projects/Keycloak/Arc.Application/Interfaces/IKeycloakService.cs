using Arc.Domain.Entities;

namespace Arc.Application.Interfaces
{
    public interface IKeycloakService
    {
        Task<string> GetAdminTokenAsync();
        Task CreateRealmAsync(string realmName);
        Task CreateClientAsync(string clientName);
        Task CreateRoleForClientAsync(string clientName, string roleName);
        Task CreateRoleAsync(string roleName);
        Task CreateGroupAsync(string groupName);
        Task AssignRoleToGroupAsync(string groupId, string roleName);
        Task AssignRolesToGroupByClientAsync(string groupName, string clientId, params string[] roleNames);
        Task CreateUserAsync(User user, string groupId);
    }
}
