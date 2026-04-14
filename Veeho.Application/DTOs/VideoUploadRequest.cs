using Microsoft.AspNetCore.Http;

namespace Veeho.Application.DTOs
{
    public class VideoUploadRequest
    {
        public IFormFile VideoFile { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public Guid UserId { get; set; }
    }
}
