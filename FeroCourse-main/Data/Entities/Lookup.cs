using System.ComponentModel.DataAnnotations;

namespace FeroCourse.Data.Entities
{
    public class Lookup
    {
        [Key]
        public int Id { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }

        public string? Category { get; set; }
        public string? Description { get; set; }
        public int OrderNo { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}