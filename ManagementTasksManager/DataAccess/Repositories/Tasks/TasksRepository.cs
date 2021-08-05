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
        public Task<IEnumerable<MaintenanceTask>> GetTasks()
        {
            return Task.FromResult(Tasks);
        }

        public Task<IEnumerable<MaintenanceTask>> OwnTasks(string owner)
        {
            return Task.FromResult(Tasks.Where(task => task.Owner == owner));
        }

        public Task<MaintenanceTask> GetTask(string id)
        {
            return Task.FromResult(Tasks.FirstOrDefault(task => task.Id == id));
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