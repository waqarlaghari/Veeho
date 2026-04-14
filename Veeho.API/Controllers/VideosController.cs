using Microsoft.AspNetCore.Mvc;
using Veeho.Application.DTOs;
using Veeho.Domain.Entities;
using Veeho.Persistence;

namespace Veeho.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        public VideosController(IWebHostEnvironment env, AppDbContext context)
        {
            _env = env;
            _context = context;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadVideo([FromForm] VideoUploadRequest request)
        {
            if (request.VideoFile == null || request.VideoFile.Length == 0)
                return BadRequest("No video uploaded.");

            var uploadsFolder = Path.Combine(_env.WebRootPath, "videos");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid() + Path.GetExtension(request.VideoFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.VideoFile.CopyToAsync(stream);
            }
            var video = new Video
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                FilePath = $"/videos/{fileName}",
                UploadedAt = DateTime.UtcNow,
                UserId = request.UserId
            };
            _context.Videos.Add(video);
            await _context.SaveChangesAsync();

            return Ok(new { video.Id, video.Title, video.FilePath });
        }
    }
}
