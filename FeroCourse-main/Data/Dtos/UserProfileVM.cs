using Microsoft.VisualBasic;

namespace FeroCourse.Data.Dtos
{
    public class UserProfileVM
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ProfilePicturePath { get; set; } = string.Empty;
        public IFormFile? ProfileImage { get; set; }

    }
}
