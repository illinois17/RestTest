using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace RestTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public UsersController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            string accessToken = "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";
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AppName", "1.0"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var response = await client.GetAsync("https://api.github.com/users");
            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadFromJsonAsync<User[]>();
                return Ok(users);
            }

            return StatusCode((int)response.StatusCode);
        }
    }
    public class User
    {
        public string Login { get; set; }
        public int Id { get; set; }
    }
}
