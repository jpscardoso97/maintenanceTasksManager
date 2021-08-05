namespace DataAccess.Repositories.Tasks.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataAccess.Models;

    public interface ITasksRepository
    {
        public Task<IEnumerable<MaintenanceTask>> GetTasks();

        public Task<IEnumerable<MaintenanceTask>> OwnTasks(string owner);

        public Task<MaintenanceTask> GetTask(string id);
        
        public Task<MaintenanceTask> CreateTask(MaintenanceTask task);
    }
}