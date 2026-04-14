
namespace Veeho.Application.DTOs
{
    public class CreateUserProfileDto
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public bool IsPublic { get; set; }
    }
}
