using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthApplication _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthApplication authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var result = await _authService.LoginAsync(userLogin);
                
                if (result == null)
                {
                    return Unauthorized(new { message = "Invalid username or password." });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login");
                return StatusCode(500, new { message = "An error occurred during login." });
            }
        }

        [HttpPost("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            try
            {
                string authHeader = Request.Headers.Authorization.ToString();
                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return Unauthorized(new { message = "Authorization header is missing or invalid." });
                }

                string accessToken = authHeader["Bearer ".Length..].Trim();


                var result = await _authService.LogoutAsync(accessToken);

                if (!result)
                {
                    return Unauthorized(new { message = "Invalid username or password." });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login");
                return StatusCode(500, new { message = "An error occurred during login." });
            }
        }
    }
}
