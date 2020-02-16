using BFFMobileApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BFFMobileApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private string _customerAPIUrl = "https://localhost:44398/Customer";

        private readonly ILogger<CustomerController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerController(ILogger<CustomerController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _httpClientFactory = httpClientFactory ?? throw new System.ArgumentNullException(nameof(httpClientFactory));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> Get()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _customerAPIUrl);

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var customerDto = JsonConvert.DeserializeObject<IEnumerable<CustomerDto>>(responseString);
                return Ok(customerDto);
            }
            else
                return NoContent();
        }
    }
}
