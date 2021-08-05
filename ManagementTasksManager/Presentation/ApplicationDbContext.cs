namespace ManagementTasksManager
{
    using ManagementTasksManager.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)  
        {  
  
        }  
        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);  
        }  
        public DbSet<User> Users { get; set; }  
    }
}