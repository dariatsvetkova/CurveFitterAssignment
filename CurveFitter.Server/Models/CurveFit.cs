using System.Text.Json.Serialization;

namespace CurveFitter.Server.Models
{
    public class DataPoint(double x, double y)
    {
        [JsonPropertyName("X")]
        public double X { get; set; } = x;

        [JsonPropertyName("Y")]
        public double Y { get; set; } = y;
    }

    public class CurveInputs
    {
        [JsonPropertyName("FitType")]
        public readonly int FitType;

        [JsonPropertyName("UserDataPoints")]
        public readonly DataPoint[] UserInputs = [];
    }

    public class CurveFit
    {
        [JsonPropertyName("Equation")]
        public required double[] Equation { get; set; }

        [JsonPropertyName("UserDataPoints")]
        public required DataPoint[] UserDataPoints { get; set; }

        [JsonPropertyName("FitDataPoints")]
        public required DataPoint[] FitDataPoints { get; set; }
    }
}
