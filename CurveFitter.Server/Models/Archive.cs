using System.ComponentModel.DataAnnotations.Schema;

namespace CurveFitter.Server.Models
{
    public class Archive : CurveFit
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public User User { get; set; } = new User();
    }
}
