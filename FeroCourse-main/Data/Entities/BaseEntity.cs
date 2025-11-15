namespace FeroCourse.Data.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid Createdby { get; set; }
        public DateTime UpdateddAt { get; set; }
        public Guid Updatedby { get; set; }
    }
}
