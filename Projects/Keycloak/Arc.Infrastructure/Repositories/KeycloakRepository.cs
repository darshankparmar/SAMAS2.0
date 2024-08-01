using System.Net.Http.Headers;
using System.Text;
using Arc.Domain.Entities;
using Arc.Domain.Interfaces;
using Newtonsoft.Json;

namespace Arc.Infrastructure.Repositories
{
    public class KeycloakRepository : IKeycloakRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _keycloakUrl = "http://localhost:8080";
        private readonly string _adminUsername = "admin"; // replace with your admin username
        private readonly string _adminPassword = "admin"; // replace with your admin password
        private readonly string _clientId = "admin-cli"; // Keycloak client ID for admin actions
        private readonly string _realmName = "ARC"; // Define the realm name

        public KeycloakRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAdminTokenAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_keycloakUrl}/realms/master/protocol/openid-connect/token")
            {
                Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("username", _adminUsername),
                    new KeyValuePair<string, string>("password", _adminPassword),
                })
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic responseJson = JsonConvert.DeserializeObject(responseContent) ?? string.Empty;
            return responseJson?.access_token ?? string.Empty;
        }

        public async Task CreateRealmAsync(string realmName)
        {
            var token = await GetAdminTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_keycloakUrl}/admin/realms")
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) },
                Content = new StringContent(JsonConvert.SerializeObject(new { id = realmName, realm = realmName }), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateClientAsync(string clientName)
        {
            var token = await GetAdminTokenAsync();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_keycloakUrl}/admin/realms/{_realmName}/clients")
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) },
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    clientId = clientName,
                    name = $"${{client_{clientName}}}",
                    enabled = true,
                    publicClient = false,
                    serviceAccountsEnabled = true
                }), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateRoleAsync(string roleName)
        {
            var token = await GetAdminTokenAsync();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_keycloakUrl}/admin/realms/{_realmName}/roles")
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) },
                Content = new StringContent(JsonConvert.SerializeObject(new { name = roleName }), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateRoleForClientAsync(string clientName, string roleName)
        {
            var token = await GetAdminTokenAsync();

            // Get client ID by clientId
            var clientRequest = new HttpRequestMessage(HttpMethod.Get, $"{_keycloakUrl}/admin/realms/{_realmName}/clients?clientId={clientName}");
            clientRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var clientResponse = await _httpClient.SendAsync(clientRequest);
            clientResponse.EnsureSuccessStatusCode();
            var clientContent = await clientResponse.Content.ReadAsStringAsync();
            dynamic clientJson = JsonConvert.DeserializeObject(clientContent);

            string clientUuid = clientJson[0].id; // Assuming clientJson returns an array, get the ID of the first client

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_keycloakUrl}/admin/realms/{_realmName}/clients/{clientUuid}/roles")
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) },
                Content = new StringContent(JsonConvert.SerializeObject(new { name = roleName }), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateGroupAsync(string groupName)
        {
            var token = await GetAdminTokenAsync();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_keycloakUrl}/admin/realms/{_realmName}/groups")
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) },
                Content = new StringContent(JsonConvert.SerializeObject(new { name = groupName }), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task AssignRoleToGroupAsync(string groupId, string roleName)
        {
            var token = await GetAdminTokenAsync();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_keycloakUrl}/admin/realms/{_realmName}/groups/{groupId}/role-mappings/realm")
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) },
                Content = new StringContent(JsonConvert.SerializeObject(new[] { new { name = roleName } }), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        private async Task<string> FindClientUuidAsync(string clientName)
        {
            var token = await GetAdminTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{_keycloakUrl}/admin/realms/{_realmName}/clients")
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) },
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var clients = JsonConvert.DeserializeObject<List<dynamic>>(responseContent);

            // Find client by name and return its UUID
            foreach (var client in clients)
            {
                if (client?.clientId == clientName)
                {
                    return client?.id;
                }
            }

            throw new KeyNotFoundException($"Client with name '{clientName}' not found.");
        }

        private async Task<string> GetRoleIdAsync(string clientId, string roleName)
        {
            var token = await GetAdminTokenAsync();
            
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_keycloakUrl}/admin/realms/{_realmName}/clients/{clientId}/roles/{roleName}")
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) },
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Content: {responseContent}");
            var role = JsonConvert.DeserializeObject<dynamic>(responseContent);

            return role?.id ?? string.Empty;
        }

        private async Task<string> GetGroupIdByNameAsync(string groupName)
        {
            var token = await GetAdminTokenAsync();
            
            // Send a GET request to retrieve all groups
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_keycloakUrl}/admin/realms/{_realmName}/groups?search={groupName}")
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) }
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var groups = JsonConvert.DeserializeObject<List<dynamic>>(responseContent);

            // Find the group by name
            var group = groups.FirstOrDefault(g => g.name == groupName);

            return group?.id ?? string.Empty;
        }

        public async Task AssignRolesToGroupByClientAsync(string groupId, string clientId, params string[] roleNames)
        {
            string groupUuid = await GetGroupIdByNameAsync(groupId);
            List<(string id, string name)> roleMappings = new List<(string id, string name)>();
            string clientUuid = await FindClientUuidAsync(clientId);
            foreach (var roleName in roleNames)
            {
                var roleId = await GetRoleIdAsync(clientUuid, roleName);
                if (!string.IsNullOrEmpty(roleId))
                {
                    roleMappings.Add((roleId, roleName));
                }
            }

            var token = await GetAdminTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_keycloakUrl}/admin/realms/{_realmName}/groups/{groupUuid}/role-mappings/clients/{clientUuid}")
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) },
                Content = new StringContent(JsonConvert.SerializeObject(roleMappings.Select(role => new 
                { 
                    id = role.id, 
                    name = role.name 
                })), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateUserAsync(User user, string groupId)
        {
            var token = await GetAdminTokenAsync();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_keycloakUrl}/admin/realms/{_realmName}/users")
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) },
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    username = user.Email,
                    email = user.Email,
                    firstName = user.Name,
                    credentials = new[] { new { type = "password", value = user.Password } },
                    groups = new[] { groupId },
                    enabled = user.IsEnabled
                }), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

    }
}
