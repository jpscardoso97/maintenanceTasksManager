namespace ApplicationService.Services.Interfaces
{
    using DataAccess.Models;

    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}