using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectC.Controllers
{
    [Route("api")]
    [ApiController]
    public class Api : ControllerBase
    {
        private HttpClient _httpClient;
        public Api(HttpClient httpClient)
        {
            
            _httpClient = httpClient;
        }

        [HttpGet("endpoint1")]
        public IActionResult Endpoint1()
        {
            return Ok("ProjectC_Endpoint1");
        }

        [HttpGet("endpoint2")]
        public IActionResult Endpoint2()
        {
            return Ok("ProjectC_Endpoint2");
        }

        [HttpGet("service_discovery")]
        public async Task<IActionResult> ServiceDiscovery()
        {
            var response = await _httpClient.GetStringAsync("http://projA/api/endpoint1");    
            return Ok(response);
        }   

    }
}
