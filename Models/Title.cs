using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestAndal.API.Models
{
    public class Title
    {
        [Key]
        public int Id { get; set; }

        public string TitleCode { get; set; }

        public string TitleName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Position> positions { get; set; }

    }
}
