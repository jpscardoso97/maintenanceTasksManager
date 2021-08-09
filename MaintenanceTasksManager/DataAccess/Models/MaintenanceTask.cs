namespace DataAccess.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class MaintenanceTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        
        public string Owner { get; set; }
        
        [Encrypted]
        public string Summary { get; set; }
        
        public DateTime Date { get; set; }
    }
}