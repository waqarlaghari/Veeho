using Microsoft.AspNetCore.Identity;

namespace Veeho.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // You can extend with more profile-specific properties as needed
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
    }
}
