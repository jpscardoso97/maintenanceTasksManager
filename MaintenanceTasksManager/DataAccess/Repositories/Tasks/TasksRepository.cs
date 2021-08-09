namespace DataAccess.Repositories.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataAccess.Models;
    using DataAccess.Repositories.Tasks.Interfaces;

    public class TasksRepository : ITasksRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TasksRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IQueryable<MaintenanceTask> GetTasks()
        {
            return _dbContext.Tasks.AsQueryable();
        }

        public IQueryable<MaintenanceTask> OwnTasks(string owner)
        {
            return _dbContext.Tasks.Where(task => task.Owner == owner);
        }

        public async Task<MaintenanceTask> GetTaskAsync(string id)
        {
            return await _dbContext.Tasks.FindAsync(id);
        }

        public async Task<MaintenanceTask> CreateTaskAsync(MaintenanceTask task)
        {
            var result = await _dbContext.Tasks.AddAsync(task);

            await _dbContext.SaveChangesAsync();

            return result?.Entity;
        }

        private static IEnumerable<MaintenanceTask> Tasks = new[]
        {
            new MaintenanceTask
            {
                Id = "1",
                Date = DateTime.Now,
                Owner = "tech1",
                Summary = "summary of the maintenance task 1"
            },
            new MaintenanceTask
            {
                Id = "2",
                Date = DateTime.Now,
                Owner = "tech1",
                Summary = "summary of the maintenance task 2"
            },
            new MaintenanceTask
            {
                Id = "3",
                Date = DateTime.Now,
                Owner = "tech2",
                Summary = "summary of the maintenance task 3"
            },
            new MaintenanceTask
            {
                Id = "4",
                Date = DateTime.Now,
                Owner = "tech3",
                Summary = "summary of the maintenance task 4"
            },
            new MaintenanceTask
            {
                Id = "5",
                Date = DateTime.Now,
                Owner = "tech4",
                Summary = "summary of the maintenance task 5"
            },
            new MaintenanceTask
            {
                Id = "6",
                Date = DateTime.Now,
                Owner = "",
                Summary = ""
            }
        };
    }
}