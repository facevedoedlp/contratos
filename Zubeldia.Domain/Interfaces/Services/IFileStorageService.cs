namespace Zubeldia.Domain.Interfaces.Services
{
    using Microsoft.AspNetCore.Http;

    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file, string containerName);
        Task DeleteFileAsync(string fileRoute, string containerName);
        Stream GetFileStreamAsync(string fileRoute);
        Task<byte[]> GetFileAsBytesAsync(string fileRoute, string containerName);
    }
}
