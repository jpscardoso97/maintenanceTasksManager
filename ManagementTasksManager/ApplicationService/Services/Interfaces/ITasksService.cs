namespace ApplicationService.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApplicationService.Models.Tasks;

    public interface ITasksService
    {
        public Task<IEnumerable<MaintenanceTask>> GetTasks();
        
        public Task<IEnumerable<MaintenanceTask>> OwnTasks(string owner);
        
        public Task<MaintenanceTask> GetTask(string id);
    }
}