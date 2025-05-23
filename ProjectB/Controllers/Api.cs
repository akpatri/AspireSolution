using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectB.Model;
using ProjectB.Repository;
using ProjectB.Services;
using GrpcService;
namespace ProjectA.Controllers
{
    [Route("api")]
    [ApiController]
    public class Api : ControllerBase
    {

        private MessageService _messageService;
        private ICustomerRepository _customerRepository;
        private readonly GrpcClient _grpcClient;
        public Api(ICustomerRepository customerRepository, MessageService messageService, GrpcClient grpcClient)
        {
            _customerRepository = customerRepository;
            _messageService = messageService;   
            _grpcClient = grpcClient;
        }

        [HttpGet("grpcEndpont")]
        public async Task<IActionResult> GrpcEndpoint()
        {
            HelloReply grpcResponse = await _grpcClient.GetGrpcResponse(new HelloRequest { Name = "ProjectB" });
            return Ok(grpcResponse);
        }


        [HttpGet("messaging")]
        public IActionResult Endpoint2(string msg)
        {
            _messageService.SendMessage(msg);
            return Ok("Messaging successful");
        }


        [HttpPost("PostAtMySql")]
        public async Task<IActionResult> PostAtMySql([FromBody] CustomerModel customer)
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