using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestAndal.API.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }

        public string PositionCode { get; set; }
        public string PositionName { get; set; }

        public int TitleId { get; set; }
        [ForeignKey("TitleId")]
        [JsonIgnore]

        public virtual Title Title {get; set;}


    }
}
