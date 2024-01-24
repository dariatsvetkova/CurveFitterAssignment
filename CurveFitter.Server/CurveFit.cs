using System.Text.Json.Serialization;

namespace CurveFitter.Server
{
    public class DataPoint
    {
        [JsonPropertyName("X")]
        public int X { get; set; }

        [JsonPropertyName("Y")]
        public int Y { get; set; }

        public DataPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class CurveFit
    {
        [JsonPropertyName("Equation")]
        public required int[] Equation { get; set; }

        [JsonPropertyName("UserDataPoints")]
        public required DataPoint[] UserDataPoints { get; set; }

        [JsonPropertyName("FitDataPoints")]
        public required DataPoint[] FitDataPoints { get; set; }
    }
}
