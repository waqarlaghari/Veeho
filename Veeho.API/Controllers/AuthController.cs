using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Veeho.Application.DTOs.Auth;
using Veeho.Application.Interfaces;
using Veeho.Domain.Entities;

namespace Veeho.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        public AuthController(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            var user = new User { Username = model.Email, Email = model.Email, FullName = model.FullName };
            var result = await _userManager.CreateAsync(user, model.Password);
            if(!result.Succeeded)
                return BadRequest(result);
            await _userManager.AddToRoleAsync(user, "viewer");
            var token = _tokenService.GenerateToken(user);
            return Ok(new AuthResponse { Token = token, ExpiresAt = DateTime.UtcNow.AddMinutes(60) });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized("Invalid credentials.");
            var token = _tokenService.GenerateToken(user);
            return Ok(new AuthResponse { Token = token, ExpiresAt = DateTime.UtcNow.AddMinutes(60) });
        }
    }
}
