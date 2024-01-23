using Microsoft.AspNetCore.Mvc;
using MathNet.Numerics;

// https://numerics.mathdotnet.com/api/MathNet.Numerics/Fit.htm

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
            string userFitType = "quadratic";
            double[] userInputsX = [1, 2, 3, 4, 5];
            double[] userInputsY = [1, 4, 8, 17, 25];

            double[] fitEquation = Fit.Polynomial(userInputsX, userInputsY, 2);

            DataPoint[] fitPoints = userInputsX.Select(x => new DataPoint(
                x,
                fitEquation[0] + x * fitEquation[1] + Math.Pow(x, 2) * fitEquation[2]
            )).ToArray();

            DataPoint[] userPoints = userInputsX.Zip(userInputsY, (x, y) => new DataPoint(x, y)).ToArray();

            CurveFit result = new CurveFit
            {
                Equation = fitEquation,
                UserDataPoints = userPoints,
                FitDataPoints = fitPoints,
            };
            return new JsonResult(result);
        }
    }
}
