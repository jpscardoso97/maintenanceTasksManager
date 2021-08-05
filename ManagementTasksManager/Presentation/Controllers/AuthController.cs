namespace ManagementTasksManager.Controllers
{
    using System.Threading.Tasks;
    using ApplicationService.Services.Interfaces;
    using ManagementTasksManager.Models;
    using Microsoft.AspNetCore.Mvc;
    using UserMapper = ManagementTasksManager.Mappers.UserMapper;

    public class AuthController : Controller
    {
        private readonly IUsersService _usersService;
        
        public AuthController(IUsersService usersService
            )
        {
            _usersService = usersService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var auth = await _usersService.Authenticate(UserMapper.ToDto(model));

            if (auth == default)
            {
                return NotFound("Invalid login");
            }

            return auth;
        }
    }
}