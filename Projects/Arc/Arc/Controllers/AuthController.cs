using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Arc.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly HttpClient _httpClient;

        private readonly string _tokenEndpoint;

        public AuthController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _tokenEndpoint = $"http://localhost:8080/realms/myrealm/protocol/openid-connect/token";
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            var formData = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", "myclient"),
            new KeyValuePair<string, string>("client_secret", "qKIrsUhziYlhrhH8maVMkB5QRvzePXQn"),
            new KeyValuePair<string, string>("username", request.Username),
            new KeyValuePair<string, string>("password", request.Password)
        });

            var response = await _httpClient.PostAsync(_tokenEndpoint, formData);
            string content = await response.Content.ReadAsStringAsync();
            
            if (response.IsSuccessStatusCode)
            {
                using JsonDocument doc = JsonDocument.Parse(content);
                JsonElement root = doc.RootElement;
                string accessToken = root.GetProperty("access_token").GetString() ?? string.Empty;
                var result = new
                {
                    token = accessToken
                };
                return Ok(result);
            }
            else
                return BadRequest(content);
        }
    }

    public class LoginRequest
    {

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

    }

}