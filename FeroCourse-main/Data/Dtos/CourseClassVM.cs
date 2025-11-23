namespace FeroCourse.Data.Dtos
{
    public class CourseClassVM
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string? ClassTitle { get; set; }
        public string? Description { get; set; }
        public string? VideoUrl { get; set; } // can be YouTube/Vimeo link or local path
        public string? DocumentUrl { get; set; } // path for PDF/PPT/ZIP
        public int OrderNo { get; set; }
        public string? Duration { get; set; }
        public bool IsFreePreview { get; set; } = false;
        public bool IsPublished { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
