using System.ComponentModel.DataAnnotations;

namespace FeroCourse.Data.Entities
{
    public class Category : BaseEntity
    {
        [Key]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public bool CategoryIsActive { get; set; } = true;


       
    }
}
