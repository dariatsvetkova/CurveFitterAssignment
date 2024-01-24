using Microsoft.AspNetCore.Mvc;
using MathNet.Numerics;

// https://numerics.mathdotnet.com/api/MathNet.Numerics/Fit.htm

namespace CurveFitter.Server.Controllers

{
    [ApiController]
    [Route("api/curvefit")]
    public class CurveFitController : ControllerBase
    {
        public static readonly int CONVERSION_COEFF = 100000;

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
            
            int[] userInputsXInt = [1 * CONVERSION_COEFF, 2 * CONVERSION_COEFF, 3 * CONVERSION_COEFF, 4 * CONVERSION_COEFF];
            int[] userInputsYInt = [1 * CONVERSION_COEFF, 4 * CONVERSION_COEFF, 8 * CONVERSION_COEFF, 17 * CONVERSION_COEFF];

            double[] userInputsX = userInputsXInt
                .Select(x => Convert.ToDouble(x) / CONVERSION_COEFF)
                .ToArray();
            double[] userInputsY = userInputsYInt
                .Select(y => Convert.ToDouble(y) / CONVERSION_COEFF)
                .ToArray();
            
            double[] fitEquation = Fit.Polynomial(userInputsX, userInputsY, 2);
            int[] fitEquationInt = fitEquation
                .Select(c => Convert.ToInt32(c * CONVERSION_COEFF))
                .ToArray();

            DataPoint[] fitPoints = userInputsX.Select(x => {
                int xInt = Convert.ToInt32(x * CONVERSION_COEFF);
                int yInt = Convert.ToInt32(fitEquation[0] + x * fitEquation[1] + Math.Pow(x, 2) * fitEquation[2]);
                return new DataPoint(xInt, yInt);
            }).ToArray();

            DataPoint[] userPoints = userInputsXInt
                .Zip(userInputsYInt, (x, y) => new DataPoint(x, y))
                .ToArray();

            CurveFit result = new CurveFit
            {
                Equation = fitEquationInt,
                UserDataPoints = userPoints,
                FitDataPoints = fitPoints,
            };
            return new JsonResult(result);
        }
    }
}
