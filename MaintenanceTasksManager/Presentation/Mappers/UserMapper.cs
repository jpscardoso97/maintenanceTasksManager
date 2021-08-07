namespace ManagementTasksManager.Mappers
{
    using ManagementTasksManager.Models;

    public static class UserMapper
    {
        public static User FromDto(ApplicationService.Models.Auth.User dto) => new()
        {
            Username = dto.Username,
            Password = dto.Password,
            Role = dto.Role
        };

        public static ApplicationService.Models.Auth.User ToDto(User dto) => new()
        {
            Username = dto.Username,
            Password = dto.Password,
            Role = dto.Role
        };
    }
}