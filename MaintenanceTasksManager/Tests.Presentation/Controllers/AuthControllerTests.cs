namespace Tests.Presentation.Controllers
{
    using System.Threading.Tasks;
    using ApplicationService.Models.Auth;
    using ApplicationService.Services.Interfaces;
    using ManagementTasksManager.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using MoqMeUp;
    using Xunit;

    public class AuthControllerTests: MoqMeUp<AuthController>
    {
        [Fact]
        public async Task Authenticate_AuthServiceReturnsNull_ReturnForbidden()
        {
            // Arrange
            this.Get<IAuthService>()
                .Setup(authService => authService.Authenticate(It.IsAny<User>()))
                .ReturnsAsync(default(Auth));
            
            // Act
            var result = await this.Build().Authenticate(new ManagementTasksManager.Models.User());
            
            // Assert
            Assert.True(result.Result is ForbidResult);
        }
        
        [Fact]
        public async Task Authenticate_AuthServiceReturnsUser_ReturnAuthWithToken()
        {
            // Arrange
            var auth = new Auth
            {
                Token = "tokenxpto",
                User = "username",
                Role = "manager"
            };
            this.Get<IAuthService>()
                .Setup(authService => authService.Authenticate(It.IsAny<User>()))
                .ReturnsAsync(auth);
            
            // Act
            var result = await this.Build().Authenticate(new ManagementTasksManager.Models.User());
            
            // Assert
            Assert.Equal(auth, result.Value);
        }
    }
}