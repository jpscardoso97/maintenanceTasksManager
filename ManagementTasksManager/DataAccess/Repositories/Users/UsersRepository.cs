﻿namespace DataAccess.Repositories.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataAccess.Models;
    using DataAccess.Repositories.Users.Interfaces;

    public class UsersRepository: IUsersRepository
    {
        public Task<User> Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "batman", Password = "batman", Role = "manager" });
            users.Add(new User { Id = 2, Username = "robin", Password = "robin", Role = "employee" });
            return Task.FromResult(users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault());

        }
    }
}