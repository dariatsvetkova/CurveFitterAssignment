using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CurveFitter.Server.Models
{
    public class ArchiveToSave : CurveFit
    {
        [JsonPropertyName("Name")]
        public required string Name { get; set; }

        [JsonPropertyName("UserId")]
        [ForeignKey("UserId")]
        public required int UserId { get; set; }

        [JsonPropertyName("FitType")]
        public required int FitType { get; set; }
    }

    public class Archive : ArchiveToSave
    {
        [JsonPropertyName("Id")]
        public required int Id { get; set; }

        [JsonPropertyName("Timestamp")]
        public required DateTime Timestamp { get; set; }
    }
}
