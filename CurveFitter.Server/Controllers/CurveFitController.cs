using Microsoft.AspNetCore.Mvc;

namespace CurveFitter.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public CurveFit Get()
        {
            return new CurveFit
            {
                Equation = [2, 3, 5, 6, 0],
                UserDataPoints = [ new DataPoint(0, 2), new DataPoint(1, 3), new DataPoint(2, 5) ],
                FitDataPoints = [new DataPoint(0, 1), new DataPoint(1, 2), new DataPoint(2, 5)]
            };
        }
    }
}
