namespace Tests.Presentation.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using ApplicationService.Models.Tasks;
    using ApplicationService.Services.Interfaces;
    using ManagementTasksManager.Controllers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using MoqMeUp;
    using Xunit;

    public class TasksControllerTests : MoqMeUp<TasksController>
    {
        private readonly TasksController _target;
        
        public TasksControllerTests()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "manager"),
            }, "mock"));
            
            _target = this.Build();
            _target.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }
        
        [Fact]
        public async Task Tasks_TasksServiceReturnsNull_ReturnsErrorCode()
        {
            // Arrange
            this.Get<ITasksService>()
            .Setup(authService => authService.GetTasks())
                .ReturnsAsync(default(IEnumerable<MaintenanceTask>));
            
            // Act
            var tasks = await _target.Tasks();
            
            // Assert
            Assert.True((tasks.Result as StatusCodeResult).StatusCode == 500);
        }
        
        [Fact]
        public async Task Tasks_TasksServiceReturnsTaskList_ReturnsTasks()
        {
            // Arrange
            var taskReturn = new List<MaintenanceTask>()
            {
                new()
                {
                    Summary = "summary",
                    Date = DateTime.Today
                }
            };
            
            this.Get<ITasksService>()
                .Setup(authService => authService.GetTasks())
                .ReturnsAsync(taskReturn);
            
            // Act
            var tasks = await _target.Tasks();
            
            // Assert
            Assert.Equal(taskReturn, tasks.Value);
        }
        
        [Fact]
        public async Task OwnTasks_TasksServiceReturnsNull_ReturnsErrorCode()
        {
            // Arrange
            this.Get<ITasksService>()
                .Setup(authService => authService.OwnTasks(It.IsAny<string>()))
                .ReturnsAsync(default(IEnumerable<MaintenanceTask>));
            
            // Act
            var tasks = await _target.OwnTasks();
            
            // Assert
            Assert.True((tasks.Result as StatusCodeResult).StatusCode == 500);
        }
        
        [Fact]
        public async Task OwnTasks_TasksServiceReturnsTaskList_ReturnsTasks()
        {
            // Arrange
            var taskReturn = new List<MaintenanceTask>()
            {
                new()
                {
                    Summary = "summary",
                    Date = DateTime.Today
                }
            };
            
            this.Get<ITasksService>()
                .Setup(authService => authService.OwnTasks(It.IsAny<string>()))
                .ReturnsAsync(taskReturn);
            
            // Act
            var tasks = await _target.OwnTasks();
            
            // Assert
            Assert.Equal(taskReturn, tasks.Value);
        }
        
        [Fact]
        public async Task CreateTask_TasksServiceThrowsException_ReturnsErrorCode()
        {
            // Arrange
            const string exceptionMessage = "error";
            
            this.Get<ITasksService>()
                .Setup(authService => authService.CreateTask(It.IsAny<MaintenanceTask>()))
                .ThrowsAsync(new ArgumentException(exceptionMessage));
            
            // Act
            var task = await _target.CreateTask(new ManagementTasksManager.Models.MaintenanceTask());
            
            // Assert
            var result = (task.Result as ObjectResult);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal(exceptionMessage, result.Value);
        }
        
        [Fact]
        public async Task CreateTask_TasksServiceReturnsNewTask_ReturnsTask()
        {
            // Arrange
            var returnTask = new MaintenanceTask
            {
                Id = "",
                Date = DateTime.Today,
                Owner = "tech1",
                Summary = "Summary"
            };
            
            this.Get<ITasksService>()
                .Setup(authService => authService.CreateTask(It.IsAny<MaintenanceTask>()))
                .ReturnsAsync(returnTask);
            
            // Act
            var task = await _target.CreateTask(new ManagementTasksManager.Models.MaintenanceTask());
            
            // Assert
            Assert.Equal(returnTask.Id, (task.Value as MaintenanceTask).Id);
            Assert.Equal(returnTask.Owner, (task.Value as MaintenanceTask).Owner);
            Assert.Equal(returnTask.Summary, (task.Value as MaintenanceTask).Summary);
            Assert.Equal(returnTask.Date, (task.Value as MaintenanceTask).Date);
        }
    }
}