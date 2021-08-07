namespace ApplicationService.Services.Interfaces
{
    using System.Threading.Tasks;
    using ApplicationService.Models.Auth;

    public interface IAuthService
    {
        public Task<Auth> Authenticate(User model);
    }
}