using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ManagementTasksManager.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApplicationService.Services.Interfaces;
    using ManagementTasksManager.Models;
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("[controller]")]
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
            var tasks =await _tasksService.GetTasks();
            
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
    }
}