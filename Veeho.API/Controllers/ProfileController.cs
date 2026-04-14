using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Veeho.Application.DTOs;
using Veeho.Application.Interfaces;
using Veeho.Infrastructure.Identity;

namespace Veeho.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserProfileService _profileService;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileController(IUserProfileService profileService, UserManager<ApplicationUser> userManager)
        {
            _profileService = profileService;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _profileService.GetUserProfileAsync(user.Id);
            return Ok(profile);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CreateUserProfileDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            await _profileService.CreateOrUpdateProfileAsync(user.Id, dto);
            return NoContent();
        }
    }
}
