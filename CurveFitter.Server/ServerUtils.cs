namespace CurveFitter.Server
{
    public class ServerUtils
    {
        public static readonly long centuryBegin = new DateTime(2001, 1, 1).Ticks;

        public static int GenerateId()
        {
            long timestamp = DateTime.Now.Ticks - centuryBegin;
            int id = (int)(timestamp % int.MaxValue);
            return id;
        }
    }
}
