namespace ManagementTasksManager.Controllers
{
    using System.Threading.Tasks;
    using ApplicationService.Services.Interfaces;
    using ManagementTasksManager.Models;
    using Microsoft.AspNetCore.Mvc;
    using UserMapper = ManagementTasksManager.Mappers.UserMapper;

    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService
            )
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var auth = await _authService.Authenticate(UserMapper.ToDto(model));

            if (auth == default)
            {
                return Forbid("Invalid login");
            }

            return auth;
        }
    }
}