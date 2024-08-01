using System.Threading.Tasks;
using Arc.Domain.Entities;

namespace Arc.Domain.Interfaces
{
    public interface IKeycloakRepository
    {
        Task<string> GetAdminTokenAsync();
        Task CreateRealmAsync(string realmName);
        Task CreateClientAsync(string clientName);
        Task CreateRoleAsync(string roleName);
        Task CreateRoleForClientAsync(string clientName, string roleName);
        Task CreateGroupAsync(string groupName);
        Task AssignRoleToGroupAsync(string groupId, string roleName);
        Task AssignRolesToGroupByClientAsync(string groupName, string clientId, params string[] roleNames);
        Task CreateUserAsync(User user, string groupId);
    }
}
