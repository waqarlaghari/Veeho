
namespace Veeho.Application.Interfaces.Services
{
    public interface IVideoStorageService
    {
        Task<string> SaveAsync(Stream videoStream, string fileName, string userId);
        Task<Stream> GetAsync(string videoId);
        Task<bool> DeleteAsync(string videoId);
    }
}
