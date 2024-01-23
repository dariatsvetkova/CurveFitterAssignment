using System.Text.Json.Serialization;

namespace CurveFitter.Server
{
    public class DataPoint
    {
        [JsonPropertyName("X")]
        public double X { get; set; }

        [JsonPropertyName("Y")]
        public double Y { get; set; }

        public DataPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
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
