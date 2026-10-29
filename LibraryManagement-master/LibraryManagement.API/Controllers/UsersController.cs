using LibraryManagement.API.Services;
using LibraryManagement.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO data)
        {
            try
            {
                var user = _service.Register(data);

                return Ok(new
                {
                    message = "Account created successfully",
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Username
                });
            }
            catch (ArgumentException error)
            {
                return BadRequest(new
                {
                    message = error.Message
                });
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO data)
        {
            var user = _service.Login(data);

            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password"
                });
            }

            return Ok(new
            {
                message = "Login successful",
                user.Id,
                user.Name,
                user.Email,
                user.Username
            });
        }
    }
}