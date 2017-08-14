using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class Range
    {
        [Key]
        public int Id { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }
    }
}
