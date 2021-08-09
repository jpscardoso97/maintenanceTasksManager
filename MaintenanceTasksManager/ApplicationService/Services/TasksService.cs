namespace ApplicationService.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApplicationService.Mappers;
    using ApplicationService.Models.Tasks;
    using ApplicationService.Services.Interfaces;
    using DataAccess.Repositories.Tasks.Interfaces;
    using global::Messaging.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TasksService : ITasksService
    {
        private const int MaxSize = 2500;
        private const string RoutingKey = "task.created";

        private readonly ITasksRepository _tasksRepository;
        private readonly IRabbitMQClient _rabbitMQClient;
        
        public TasksService(
            ITasksRepository tasksRepository, 
            IRabbitMQClient rabbitMQClient)
        {
            _tasksRepository = tasksRepository;
            _rabbitMQClient = rabbitMQClient;
        }
        
        public async Task<IEnumerable<MaintenanceTask>> GetTasks()
        {
            return TaskMapper.FromDto(await _tasksRepository.GetTasks().ToListAsync());
        }

        public async Task<IEnumerable<MaintenanceTask>> OwnTasks(string owner)
        {
            return TaskMapper.FromDto(await _tasksRepository.OwnTasks(owner).ToListAsync());
        }

        public async Task<MaintenanceTask> GetTask(string id)
        {
            return TaskMapper.FromDto(await _tasksRepository.GetTaskAsync(id));
        }
        
        public async Task<MaintenanceTask> CreateTask(MaintenanceTask task)
        {
            if (ValidateTask(task))
                throw new ArgumentException($"Summary is mandatory and can't have more than {MaxSize} characters");
            
            var newTask = TaskMapper.FromDto(await _tasksRepository.CreateTaskAsync(TaskMapper.ToDto(task)));
            
            if(newTask != null)
            {
                _rabbitMQClient.PushMessage(RoutingKey, $"The tech {newTask.Owner} performed the task {newTask.Id} on date {newTask.Date.ToShortDateString()}");
            }

            return newTask;
        }

        private bool ValidateTask(MaintenanceTask task) =>
            string.IsNullOrWhiteSpace(task.Summary) || task.Summary.Length > MaxSize;
    }
}