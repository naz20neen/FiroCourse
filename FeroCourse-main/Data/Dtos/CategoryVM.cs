namespace FeroCourse.Data.ViewModels
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public bool CategoryIsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid Createdby { get; set; }
        public DateTime UpdateddAt { get; set; } = DateTime.Now;

        public Guid Updatedby { get; set; }
    }
}
