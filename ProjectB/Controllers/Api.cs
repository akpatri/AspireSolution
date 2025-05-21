using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectB.Model;
using ProjectB.Repository;
using ProjectB.Services;

namespace ProjectA.Controllers
{
    [Route("api")]
    [ApiController]
    public class Api : ControllerBase
    {

        private MessageService _messageService;
        private ICustomerRepository _customerRepository;
        public Api(ICustomerRepository customerRepository, MessageService messageService)
        {
            _customerRepository = customerRepository;
            _messageService = messageService;   
        }

        [HttpGet("endpoint1")]
        public IActionResult Endpoint1()
        {
            return Ok("ProjectB_Endpoint1");
        }


        [HttpGet("messaging")]
        public IActionResult Endpoint2(string msg)
        {
            _messageService.SendMessage(msg);
            return Ok("Messaging successiful");
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