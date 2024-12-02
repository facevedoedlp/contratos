namespace Zubeldia.Services.Files
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Zubeldia.Domain.Interfaces.Services;

    public class FileStorageService : IFileStorageService
    {
        private readonly string rootPath;

        public FileStorageService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            var relativePath = configuration["RootPath"] ?? "Files";
            rootPath = Path.Combine(environment.ContentRootPath, relativePath);
        }

        public  Stream GetFileStreamAsync(string fileRoute)
        {
            if (string.IsNullOrEmpty(fileRoute)) throw new ArgumentNullException(nameof(fileRoute));

            fileRoute = fileRoute.TrimStart('/');

            var fullPath = Path.Combine(rootPath, fileRoute);

            if (!File.Exists(fullPath)) throw new FileNotFoundException("El archivo no fue encontrado", fullPath);

            var fullPathNormalized = Path.GetFullPath(fullPath);
            var rootPathNormalized = Path.GetFullPath(rootPath);

            if (!fullPathNormalized.StartsWith(rootPathNormalized)) throw new UnauthorizedAccessException("Acceso al archivo no permitido");

            return new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public async Task<string> SaveFileAsync(IFormFile file, string containerName)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(rootPath, containerName);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string route = Path.Combine(folder, fileName);
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                await File.WriteAllBytesAsync(route, ms.ToArray());
            }

            return $"/{containerName}/{fileName}";
        }

        public Task DeleteFileAsync(string fileRoute, string containerName)
        {
            if (string.IsNullOrEmpty(fileRoute))
            {
                return Task.CompletedTask;
            }

            var fileName = Path.GetFileName(fileRoute);
            var fileDirectory = Path.Combine(rootPath, containerName, fileName);

            if (File.Exists(fileDirectory))
            {
                File.Delete(fileDirectory);
            }

            return Task.CompletedTask;
        }
    }
}