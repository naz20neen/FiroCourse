namespace FeroCourse.Data.Dtos
{
    public class CourseVM
    {
            public int CourseId { get; set; }
            public int CategoryId { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public string? InstructorName { get; set; }
            public decimal Price { get; set; }
            public decimal? DiscountPrice { get; set; }
            public string? ThumbnailPath { get; set; }
            public IFormFile? Imagefile { get; set; }  
            public bool IsPublished { get; set; } = false;
            public DateTime CreatedAt { get; set; } = DateTime.Now;
        
    }
}
