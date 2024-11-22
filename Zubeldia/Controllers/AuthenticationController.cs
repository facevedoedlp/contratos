namespace Zubeldia.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Zubeldia.Domain.Dtos.Authentication;
    using Zubeldia.Domain.Interfaces.Services;
    using Zubeldia.Dtos.Models.Commons;
    using Zubeldia.Dtos.Models.User;

    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController(IAuthenticateService authenticateService)
        : ZubeldiaControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginAsync([FromBody] LoginRequest loginDto)
        {
            var response = await authenticateService.LoginAsync(loginDto);

            if (!response.Success)
            {
                return Unauthorized(response);
            }

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ValidatorResultDto>> RegisterAsync([FromBody] UserDto userDto)
        {
            var response = await authenticateService.RegisterAsync(userDto);
            return Ok(response);
        }
    }
}