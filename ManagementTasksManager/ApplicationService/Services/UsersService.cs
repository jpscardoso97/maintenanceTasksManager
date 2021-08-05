namespace ApplicationService.Services
{
    using System.Threading.Tasks;
    using ApplicationService.Models.Auth;
    using ApplicationService.Services.Interfaces;
    using DataAccess.Repositories.Users.Interfaces;

    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly TokenService _tokenService;

        public UsersService(IUsersRepository usersRepository, TokenService tokenService)
        {
            _usersRepository = usersRepository;
            _tokenService = tokenService;
        }

        public async Task<Auth> Authenticate(User model)
        {
            var user = await _usersRepository.Get(model.Username, model.Password);

            if (user == null)
                return default;

            var token = _tokenService.GenerateToken(user);

            return new Auth
            {
                User = user.Username,
                Role = user.Role,
                Token = token
            };
        }
    }
}