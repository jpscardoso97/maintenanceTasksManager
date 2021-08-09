namespace DataAccess.Repositories.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataAccess.Models;
    using DataAccess.Repositories.Users.Interfaces;

    public class UsersRepository: IUsersRepository
    {
        private readonly ApplicationDbContext _dbContext;
       
        public UsersRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Task<User> Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "manager", Password = "manager", Role = "manager" });
            users.Add(new User { Id = 2, Username = "tech1", Password = "tech1", Role = "technician" });
            users.Add(new User { Id = 2, Username = "tech2", Password = "tech2", Role = "technician" });
            users.Add(new User { Id = 2, Username = "tech3", Password = "tech3", Role = "technician" });
            return Task.FromResult(users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault());
        }
    }
}