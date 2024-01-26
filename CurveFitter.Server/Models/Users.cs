namespace CurveFitter.Server.Models
{
    public class User
    {
        public int Id { get; set; }

        public List<Archive> Archives { get; set; } = [];
    }
}
