namespace FeroCourse.ServicesInterface
{
    public interface IFileUploadService
    {
        Task<string?> UploadImageAsync(IFormFile file, string folder);
    }
}
