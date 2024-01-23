using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CurveFitter.Server.Controllers

{
    [ApiController]
    [Route("api/curvefit")]
    public class CurveFitController : ControllerBase
    {
        private static readonly string[] FitTypes =
        [
            "linear", "quadratic", "cubic",
        ];

        private readonly ILogger<CurveFitController> _logger;

        public CurveFitController(ILogger<CurveFitController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetCurveFit")]
        public IActionResult Get()
        {
            CurveFit result = new CurveFit
            {
                Equation = [2, 3, 5, 6, 0],
                UserDataPoints = [ new DataPoint(0, 2), new DataPoint(1, 3), new DataPoint(2, 5) ],
                FitDataPoints = [new DataPoint(0, 1), new DataPoint(1, 2), new DataPoint(2, 5)]
            };
            return new JsonResult(result);
        }
    }
}
