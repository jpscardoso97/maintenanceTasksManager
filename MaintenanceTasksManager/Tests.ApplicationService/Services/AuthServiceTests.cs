namespace Tests.ApplicationService.Services
{
    using System.Threading.Tasks;
    using DataAccess.Models;
    using DataAccess.Repositories.Users.Interfaces;
    using global::ApplicationService.Services;
    using global::ApplicationService.Services.Interfaces;
    using Moq;
    using MoqMeUp;
    using Xunit;

    public class AuthServiceTests : MoqMeUp<AuthService>
    {
        [Fact]
        public async Task Authenticate_UserDoesNotExist_ReturnsNull()
        {
            // Arrange
            this.Get<IUsersRepository>()
                .Setup(repo => repo.Get(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(default(User));
            
            // Act
            var result = await this.Build().Authenticate(new global::ApplicationService.Models.Auth.User());

            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public async Task Authenticate_ValidUser_ReturnsAuthWithToken()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Username = "tech1",
                Password = "tech1",
                Role = "technician"
            };
            this.Get<IUsersRepository>()
                .Setup(repo => repo.Get(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            var MockToken = "token";
            this.Get<ITokenService>()
                .Setup(tokenService => tokenService.GenerateToken(It.IsAny<User>()))
                .Returns(MockToken);
            // Act
            var result = await this.Build().Authenticate(new global::ApplicationService.Models.Auth.User());

            // Assert
            Assert.Equal(user.Username, result.User);
            Assert.Equal(user.Role, result.Role);
            Assert.Equal(MockToken, result.Token);
        }
    }
}