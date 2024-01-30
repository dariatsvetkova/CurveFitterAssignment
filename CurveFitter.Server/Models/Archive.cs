using System.ComponentModel.DataAnnotations.Schema;

namespace CurveFitter.Server.Models
{
    public class Archive : CurveFit
    {
        public required int FitType;
        public required int Id { get; set; }
        public required string Name { get; set; }
        public DateTime Timestamp { get; set; }

        [ForeignKey("UserId")]
        public required int UserId { get; set; }
    }
}
