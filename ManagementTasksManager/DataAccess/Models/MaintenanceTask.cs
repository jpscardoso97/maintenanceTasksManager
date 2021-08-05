namespace DataAccess.Models
{
    using System;

    public class MaintenanceTask
    {
        public string Id { get; set; }
        
        public string Owner { get; set; }
        
        public string Summary { get; set; }
        
        public DateTime Date { get; set; }
    }
}