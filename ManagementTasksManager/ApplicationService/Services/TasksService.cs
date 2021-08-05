namespace ApplicationService.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApplicationService.Mappers;
    using ApplicationService.Models.Tasks;
    using ApplicationService.Services.Interfaces;
    using DataAccess.Repositories.Tasks.Interfaces;

    public class TasksService : ITasksService
    {
        private const int MaxSize = 2500;
        private readonly ITasksRepository _tasksRepository;
        
        public TasksService(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }
        
        public async Task<IEnumerable<MaintenanceTask>> GetTasks()
        {
            return TaskMapper.FromDto(await _tasksRepository.GetTasks());
        }

        public async Task<IEnumerable<MaintenanceTask>> OwnTasks(string owner)
        {
            return TaskMapper.FromDto(await _tasksRepository.OwnTasks(owner));
        }

        public async Task<MaintenanceTask> GetTask(string id)
        {
            return TaskMapper.FromDto(await _tasksRepository.GetTask(id));
        }
        
        public async Task<MaintenanceTask> CreateTask(MaintenanceTask task)
        {
            if (ValidateTask(task))
                throw new ArgumentException($"Summary is mandatory and can't have more than {MaxSize} characters");
            
            var newTask = await _tasksRepository.CreateTask(TaskMapper.ToDto(task));
            
            //TODO add message for notification
        }

        private bool ValidateTask(MaintenanceTask task) =>
            string.IsNullOrWhiteSpace(task.Summary) || task.Summary.Length > MaxSize;
    }
}