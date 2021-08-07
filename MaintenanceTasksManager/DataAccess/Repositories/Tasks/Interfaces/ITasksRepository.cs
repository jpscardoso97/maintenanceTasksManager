namespace DataAccess.Repositories.Tasks.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataAccess.Models;

    public interface ITasksRepository
    {
        public IQueryable<MaintenanceTask> GetTasks();

        public IQueryable<MaintenanceTask> OwnTasks(string owner);

        public Task<MaintenanceTask> GetTaskAsync(string id);
        
        public Task<MaintenanceTask> CreateTaskAsync(MaintenanceTask task);
    }
}