using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectC.Controllers
{
    [Route("api")]
    [ApiController]
    public class Api : ControllerBase
    {
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

    }
}
