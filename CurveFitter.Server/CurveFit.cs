namespace CurveFitter.Server
{
    public class DataPoint
    {
        public int X { get; set; }

        public int Y { get; set; }

        public DataPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class CurveFit
    {
        public required int[] Equation { get; set; }

        public required DataPoint[] UserDataPoints { get; set; }

        public required DataPoint[] FitDataPoints { get; set; }
    }
}
