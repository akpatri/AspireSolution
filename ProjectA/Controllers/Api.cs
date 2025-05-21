using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectA.Controllers
{
    [Route("api")]
    [ApiController]
    public class Api : ControllerBase
    {

        [HttpGet("endpoint1")]
        public IActionResult Enpoint1()
        {
            return Ok("ProjectA_Endpoint1");
        }


        [HttpGet("endpoint2")]
        public IActionResult Enpoint2()
        {
            return Ok("ProjectA_Endpoint2");
        }
    }
}
