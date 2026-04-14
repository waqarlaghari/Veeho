
namespace Veeho.Domain.Entities
{
    public class VideoCategory
    {
        public Guid VideoId { get; set; }
        public Video? Video { get; set; }

        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
