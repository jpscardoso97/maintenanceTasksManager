namespace ApplicationService.Models.Tasks
{
    using System;

    public class MaintenanceTask
    {
        public string Id { get; set; }
        
        public string Owner { get; set; }
        
        public Summary Summary { get; set; }
        
        public DateTime Date { get; set; }
    }
}