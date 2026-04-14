using Microsoft.EntityFrameworkCore;
using Veeho.Application.DTOs;
using Veeho.Application.Interfaces;
using Veeho.Domain.Entities;
using Veeho.Persistence;

namespace Veeho.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly AppDbContext _context;
        public UserProfileService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<UserProfileDto> GetUserProfileAsync(string userId)
        {
            var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.ApplicationUserId == userId);
            return profile == null ? null : new UserProfileDto
            {
                DisplayName = profile.DisplayName,
                Bio = profile.Bio,
                ProfilePictureUrl = profile.ProfilePictureUrl
            };
        }
        public async Task CreateOrUpdateProfileAsync(string userId, CreateUserProfileDto dto)
        {
            var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.ApplicationUserId == userId);
            if(profile == null)
            {
                profile = new UserProfile
                {
                    ApplicationUserId = userId
                };
                _context.UserProfiles.Add(profile);
            }
            profile.DisplayName = dto.DisplayName;
            profile.Bio = dto.Bio;
            profile.IsPublic = dto.IsPublic;
            await _context.SaveChangesAsync();
        }
    }
}
