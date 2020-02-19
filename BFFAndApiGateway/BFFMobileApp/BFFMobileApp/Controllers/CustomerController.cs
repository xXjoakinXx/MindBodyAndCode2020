using BFFMobileApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
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
        private string _vehicleAPIUrl = "https://localhost:44384/vehicle";

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
            var requestToCustomerApi = new HttpRequestMessage(HttpMethod.Get, _customerAPIUrl);
            var requestToVechicleApi = new HttpRequestMessage(HttpMethod.Get, _vehicleAPIUrl);

            var client = _httpClientFactory.CreateClient();

            var customerResponseTask = client.SendAsync(requestToCustomerApi);
            var vehicleResponseTask = client.SendAsync(requestToVechicleApi);

            await Task.WhenAll(customerResponseTask, vehicleResponseTask);

            var customerResponse = customerResponseTask.Result;
            var vehicleResponse = vehicleResponseTask.Result;

            if (customerResponse.IsSuccessStatusCode && vehicleResponse.IsSuccessStatusCode)
            {
                IEnumerable<CustomerDto> customerDtos = await DeseralizeResponse<CustomerDto>(customerResponse);
                IEnumerable<VehicleDto> vehicleDtos = await DeseralizeResponse<VehicleDto>(vehicleResponse);

                var customerWithVehicleDtos = BuildResultDto(customerDtos, vehicleDtos);

                return Ok(customerWithVehicleDtos);
            }
            else
                return NoContent();
        }

        private IEnumerable<CustomerDto> BuildResultDto(IEnumerable<CustomerDto> customerDtos, IEnumerable<VehicleDto> vehicleDtos)
        {

            foreach(var customerDto in customerDtos)
            {
                customerDto.Vehicle = vehicleDtos.FirstOrDefault(v => v.CustomerId == customerDto.CustomerId);
            }

            return customerDtos;
        }

        private async Task<IEnumerable<T>> DeseralizeResponse<T>(HttpResponseMessage customerResponse)
        {
            var responseString = await customerResponse.Content.ReadAsStringAsync();
            var resultDto = JsonConvert.DeserializeObject<IEnumerable<T>>(responseString);
            return resultDto;
        }


    }
}
