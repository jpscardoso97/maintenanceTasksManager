namespace ApplicationService.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApplicationService.Models.Tasks;
    using ApplicationService.Services.Interfaces;

    public class TasksService : ITasksService
    {
        public TasksService(I)
        {
            
        }
        
        public Task<IEnumerable<MaintenanceTask>> GetTasks()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MaintenanceTask>> OwnTasks(string owner)
        {
            throw new System.NotImplementedException();
        }

        public Task<MaintenanceTask> GetTask(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}