using Microsoft.AspNetCore.Mvc;
using MathNet.Numerics;
using CurveFitter.Server.Models;

// https://numerics.mathdotnet.com/api/MathNet.Numerics/Fit.htm

namespace CurveFitter.Server.Controllers

{
    [ApiController]
    [Route("api/curvefit")]
    public class CurveFitController() : ControllerBase
    {
        private static (bool, string) ValidateInputs(int fitType, DataPoint[] inputs)
        {
            if (inputs.Length < fitType + 1)
            {
                return (false, $"Not enough data points: {inputs.Length}");
            }

            return (true, "");
        }

        private static (double[], double[]) ConvertInputs(DataPoint[] inputsInt)
        {
            double[] inputsX = inputsInt.Select(p => p.X).ToArray();
            double[] inputsY = inputsInt.Select(p => p.Y).ToArray();

            return ( inputsX, inputsY );
        }

        private static DataPoint[] ConvertDataPoints(double[] pointsX, double[] pointsY)
        {
            return pointsX.Select((x, ind) => {
                int xInt = Convert.ToInt32(x);
                int yInt = Convert.ToInt32(pointsY[ind]);
                return new DataPoint(xInt, yInt);
            }).ToArray();
        }

        // GET: api/curvefit?userPoints="1y-2,3y-4"&fitType=1
        [HttpGet(Name = "GetCurveFit")]
        public IActionResult Get(string userPoints, string fitType)
        {
            // Parse user inputs from the URL query string

            DataPoint[] userPointsObj = ServerUtils.StringToDataPoints(userPoints);

            int fitTypeInt = Convert.ToInt32(fitType);

            // Validate user inputs

            (bool isValid, string errorMessage) = ServerUtils.ValidateUserInputs(userPointsObj, fitTypeInt);

            if (isValid == false)
            {
                return BadRequest(errorMessage);
            }

            // Convert user inputs into separate lists so we can use them in the Fit.Polynomial method

            (double[] userPointsX, double[] userPointsY) = ConvertInputs(userPointsObj);

            // Get polynomial coefficients for the fit (where list index corresponds to the power of x)

            double[] fitEquation = new double[fitTypeInt + 1];
            fitEquation = Fit.Polynomial(userPointsX, userPointsY, fitTypeInt);

            // Note that values of X are the same for user points and fit points, so we only need to calculate Y values
            double[] fitPointsY = userPointsX
                .Select(x => fitEquation.Select((coeff, ind) => coeff * Math.Pow(x, ind)).Sum())
                .ToArray();

            DataPoint[] fitPoints = ConvertDataPoints(userPointsX, fitPointsY);
            DataPoint[] userPointsFormatted = ConvertDataPoints(userPointsX, userPointsY);

            CurveFit result = new CurveFit
            {
                Equation = fitEquation,
                UserDataPoints = userPointsFormatted,
                FitDataPoints = fitPoints,
            };

            // TBD: update serialization settings to convert properties to camelCase
            return new JsonResult(result);
        }
    }
}
