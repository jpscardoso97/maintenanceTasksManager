namespace ApplicationService.Mappers
{
    using ApplicationService.Models;
    using ApplicationService.Models.Auth;

    public static class UserMapper
    {
        public static User FromDto(DataAccess.Models.User dto) => new User
        {
            Username = dto.Username,
            Password = dto.Password,
            Role = dto.Role
        };
    }
}