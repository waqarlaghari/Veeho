
namespace Veeho.Domain.Entities
{
    public class User 
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string FullName { get; set; } = default!;

        public ICollection<Video>? Videos { get; set; }
    }
}
