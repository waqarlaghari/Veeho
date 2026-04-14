
namespace Veeho.Domain.Entities
{
    public class Video
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string FilePath { get; set; } = default!;
        public DateTime UploadedAt { get; set; }
        public Guid UserId { get; set; }
        public User? UploadedBy { get; set; }
    }
}
