using BFFWeb.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BFFWeb.Controllers
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
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

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
