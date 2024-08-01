using Arc.Application.Interfaces;
using Arc.Domain.Entities;
using Arc.Domain.Interfaces;
using System.Threading.Tasks;

namespace Arc.Application.Services
{
    public class KeycloakService : IKeycloakService
    {
        private readonly IKeycloakRepository _keycloakRepository;

        public KeycloakService(IKeycloakRepository keycloakRepository)
        {
            _keycloakRepository = keycloakRepository;
        }

        public Task<string> GetAdminTokenAsync() => _keycloakRepository.GetAdminTokenAsync();

        public Task CreateRealmAsync(string realmName) => _keycloakRepository.CreateRealmAsync(realmName);

        public Task CreateClientAsync(string clientName) => _keycloakRepository.CreateClientAsync(clientName);

        public Task CreateRoleForClientAsync(string clientName, string roleName) => _keycloakRepository.CreateRoleForClientAsync(clientName, roleName);
        
        public Task CreateRoleAsync(string roleName) => _keycloakRepository.CreateRoleAsync(roleName);

        public Task CreateGroupAsync(string groupName) => _keycloakRepository.CreateGroupAsync(groupName);

        public Task AssignRoleToGroupAsync(string groupId, string roleName) => _keycloakRepository.AssignRoleToGroupAsync(groupId, roleName);

        public Task AssignRolesToGroupByClientAsync(string groupName, string clientId, params string[] roleNames) => _keycloakRepository.AssignRolesToGroupByClientAsync(groupName, clientId, roleNames);

        public Task CreateUserAsync(User user, string groupId) => _keycloakRepository.CreateUserAsync(user, groupId);
    }
}
