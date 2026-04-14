
using Veeho.Application.DTOs;
using Veeho.Domain.Entities;

namespace Veeho.Application.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileDto> GetUserProfileAsync(string userId);
        Task CreateOrUpdateProfileAsync(string userId, CreateUserProfileDto dto);
    }
}
