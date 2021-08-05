namespace ManagementTasksManager.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApplicationService.Services.Interfaces;
    using ManagementTasksManager.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;
        private readonly ITasksService _tasksService;

        public TasksController(
            ILogger<TasksController> logger,
            ITasksService tasksService)
        {
            _logger = logger;
            _tasksService = tasksService;
        }

        [HttpGet]
        [Route("tasks")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<IEnumerable<MaintenanceTask>>> Tasks()
        {
            var tasks = await _tasksService.GetTasks();

            if (tasks == null)
                return Problem();

            return Ok(tasks);
        }

        [HttpGet]
        [Route("mytasks")]
        [Authorize(Roles = "technician")]
        public async Task<ActionResult<IEnumerable<MaintenanceTask>>> OwnTasks()
        {
            var tasks = await _tasksService.OwnTasks(User.Identity.Name);

            if (tasks == null)
                return Problem("Error getting technician's tasks");

            return Ok(tasks);
        }

        [HttpPost]
        [Route("task/create")]
        [Authorize(Roles = "technician")]
        public async Task<ActionResult<IEnumerable<MaintenanceTask>>> CreateTasks([FromBody] MaintenanceTask model)
        {
            try
            {
                var newTask = new ApplicationService.Models.Tasks.MaintenanceTask
                {
                    Owner = User.Identity.Name,
                    Date = string.IsNullOrWhiteSpace(model.Date) ? DateTime.Now : DateTime.Parse(model.Date),
                    Summary = model.Summary
                };

                return Ok(await _tasksService.CreateTask(newTask));
            }
            catch (ArgumentException e)
            {
                return Problem(e.Message);
            }
        }
    }
}