using Microsoft.AspNetCore.Identity;

namespace FeroCourse.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }

        public string? ProfilePicturePath { get; set; }
    }
}
