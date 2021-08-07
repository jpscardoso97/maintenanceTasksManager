namespace DataAccess
{
    using System.Threading.Tasks;
    using DataAccess.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext: DbContext
    {
        public DbSet<MaintenanceTask> Tasks { get; set; }  
        public DbSet<User> Users { get; set; }  
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)  
        {  
  
        }  
        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);  
        }  
    }
}