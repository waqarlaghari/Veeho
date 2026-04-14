using Veeho.Application.Interfaces.Services;

namespace Veeho.Infrastructure.Services
{
    public class LocalVideoStorageService : IVideoStorageService
    {
        public Task<string> SaveAsync(Stream videoStream, string fileName, string userId)
        {

        }
        public Task<Stream> GetAsync(string videoId)
        {

        }
        public Task<bool> DeleteAsync(string videoId)
        {

        }
    }
}
