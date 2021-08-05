namespace ApplicationService.Services.Interfaces
{
    using System.Threading.Tasks;
    using ApplicationService.Models;
    using ApplicationService.Models.Auth;

    public interface IUsersService
    {
        public Task<Auth> Authenticate(User model);
    }
}