
namespace Veeho.Domain.Entities
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool IsPublic { get; set; }

        public ApplicationUser applicationUser { get; set; }
    }
}
