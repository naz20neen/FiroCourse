using FeroCourse.ServicesInterface;

namespace FeroCourse.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _env;

        public FileUploadService(IWebHostEnvironment env)
        {
            _env = env;

        }
        public async Task<string?> UploadImageAsync(IFormFile file, string folderPath)
        {

            if (file == null || file.Length == 0)
                return null;
            string uploadfolder = Path.Combine(_env.WebRootPath, folderPath);
            if (!Directory.Exists(uploadfolder))
            {
                Directory.CreateDirectory(uploadfolder);

            }

            string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string filepath = Path.Combine(uploadfolder, filename);

            using (var steam = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(steam);

            }

            return Path.Combine(folderPath, filename).Replace("\\", "/");



        }
    }
}

