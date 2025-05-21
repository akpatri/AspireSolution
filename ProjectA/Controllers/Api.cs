using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using StackExchange.Redis;

namespace ProjectA.Controllers
{
    [Route("api")]
    [ApiController]
    public class Api : ControllerBase
    {
        private IConnectionMultiplexer _redisMus;
        public Api(IConnectionMultiplexer redisMus)
        {
            _redisMus = redisMus;
        }

        [HttpGet("endpoint1")]
        [OutputCache(Duration =30)]
        public IActionResult Enpoint1()
        {
            Thread.Sleep(5000); // Simulate a delay for testing
            return Ok("ProjectA_Endpoint1");
        }


        [HttpGet("endpoint2")]
        [OutputCache(Duration = 30)]
        public IActionResult Enpoint2()
        {
            return Ok("ProjectA_Endpoint2");
        }

        //Interaction with redis Server
        [HttpGet("redisClient-insert")]
        public IActionResult RedisClient()
        {
            var db = _redisMus.GetDatabase();
            var key = "key1";
            var value = "value1";
            // Set a value in Redis
            db.StringSet(key, value);
            // Get the value back from Redis
            var redisValue = db.StringGet(key);
            return Ok($"Redis Value: {redisValue}");
                       
        }   
    }
}
