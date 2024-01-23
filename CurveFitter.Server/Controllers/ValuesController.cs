using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace CurveFitter.Server.Controllers
{
    [Route("api/helloworld")]
    [ApiController]
    [Produces("application/json")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public Dictionary<string, string> Get()
        {
            // Return a response with a response body in JSON format that says "Hello World!"
            return new Dictionary<string, string> { { "message", "Hello World!" } };
        }
    }
}
