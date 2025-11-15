using FeroCourse.Services.Email;
using FeroCourse.ServicesInterface;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace FeroCourse.Services
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Email Service
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            // Identity EmailSender
            services.AddTransient<IEmailSender, EmailService>();

            services.AddTransient<IFileUploadService, FileUploadService>();
        }
    }
}
