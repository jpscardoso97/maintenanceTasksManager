namespace DataAccess.Repositories.Users.Interfaces
{
    using System.Threading.Tasks;
    using DataAccess.Models;

    public interface IUsersRepository
    {
        public Task<User> Get(string username, string password);
    }
}