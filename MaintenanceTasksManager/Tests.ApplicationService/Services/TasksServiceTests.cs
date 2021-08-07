namespace Tests.ApplicationService.Services
{
    using System;
    using System.Threading.Tasks;
    using DataAccess.Repositories.Tasks.Interfaces;
    using global::ApplicationService.Models.Tasks;
    using global::ApplicationService.Services;
    using Moq;
    using MoqMeUp;
    using Xunit;

    public class TasksServiceTests : MoqMeUp<TasksService>
    {
        private const string LongSummary =
            "mdoaismdoaismdoiamsodimaosidmaosiasdjoiasjdasddmoaismdoiamsodiasjdnoas8dsdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjdmdoaismdoaismdoiamsodimaosidmaosidmoaismdoiamsodiasjd";

        public TasksServiceTests()
        {
            this.Get<ITasksRepository>()
                .Setup(repository => repository.CreateTaskAsync(It.IsAny<DataAccess.Models.MaintenanceTask>()))
                .Returns<DataAccess.Models.MaintenanceTask>(Task.FromResult);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData(LongSummary)]
        public void CreateTask_InvalidSummary_TaskIsNotCreated(string summary)
        {
            // Act and Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await this.Build().CreateTask(CreateTask(summary)));
            this.Get<ITasksRepository>()
                .VerifyNoOtherCalls();
        }

        [Fact]
        public async Task CreateTask_ValidTask_TaskIsCreated()
        {
            // Arrange
            var task = CreateTask("valid summary");

            // Act
            var result = await this.Build().CreateTask(task);

            // Assert
            Assert.Equal(task.Id, result.Id);
            Assert.Equal(task.Date, result.Date);
            Assert.Equal(task.Owner, result.Owner);
            Assert.Equal(task.Summary, result.Summary);
            this.Get<ITasksRepository>()
                .Verify(repo => repo.CreateTaskAsync(It.IsAny<DataAccess.Models.MaintenanceTask>()), Times.Once);
        }

        [Fact]
        public async Task CreateTask_RepositoryReturnsNull_ReturnsNull()
        {
            // Arrange
            this.Get<ITasksRepository>()
                .Setup(repository => repository.CreateTaskAsync(It.IsAny<DataAccess.Models.MaintenanceTask>()))
                .ReturnsAsync(default(DataAccess.Models.MaintenanceTask));

            // Act
            var result = await this.Build().CreateTask(CreateTask("valid summary"));

            // Assert
            Assert.Null(result);
            this.Get<ITasksRepository>()
                .Verify(repo => repo.CreateTaskAsync(It.IsAny<DataAccess.Models.MaintenanceTask>()), Times.Once);
        }

        private MaintenanceTask CreateTask(string summary) => new()
        {
            Id = "task1",
            Date = DateTime.Now,
            Owner = "tech1",
            Summary = summary
        };
    }
}