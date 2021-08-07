namespace DataAccess.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MaintenanceTask
    {
        public string Id { get; set; }
        
        public string Owner { get; set; }
        
        [Encrypted]
        public string Summary { get; set; }
        
        public DateTime Date { get; set; }
    }
}