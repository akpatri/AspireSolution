using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectA.Controllers
{
    [Route("api")]
    [ApiController]
    public class Api : ControllerBase
    {
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
    }
}
