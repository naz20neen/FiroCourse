using FeroCourse.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FeroCourse.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categorys { get; set; }
    public DbSet<Lookup> Lookups { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseClass> Classes { get; set; }
}
