using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectB.Model;
using ProjectB.Repository;

namespace ProjectA.Controllers
{
    [Route("api")]
    [ApiController]
    public class Api : ControllerBase
    {
        private ICustomerRepository _customerRepository;
        public Api(ICustomerRepository customerRepository)
        {
            
            _customerRepository = customerRepository;   
        }

        [HttpGet("endpoint1")]
        public IActionResult Endpoint1()
        {
            return Ok("ProjectB_Endpoint1");
        }


        [HttpGet("endpoint2")]
        public IActionResult Endpoint2()
        {
            return Ok("ProjectB_Endpoint2");
        }


        [HttpPost("PostAtMySQl")]
        public async Task<IActionResult> PostAtMySQL([FromBody] CustomerModel customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer data is required.");
            }
            else { 
                await _customerRepository.AddCustomerAsync(customer);
                return Ok("Customer added successfully.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFromMySql() {
            return Ok(await _customerRepository.GetAllCustomersAsync());
        }

    }
}