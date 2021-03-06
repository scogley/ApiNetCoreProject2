using ApiNetCoreProject2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNetCoreProject2
{
    // Implements IUserManagerService so I can use dependency injection
    // and enable unit testing of the controller logic.
    // See this for example adding unit tests https://code-maze.com/unit-testing-aspnetcore-web-api/
    public class UserManagerService : IUserManagerService
    {
        static List<UserModel> userList = new List<UserModel>();
        public UserModel Add(UserModel value)
        {
            var user = new UserModel();
            user.Email = value.Email;
            user.Password = value.Password;
            user.CreatedDate = DateTime.UtcNow;
            user.UserId = System.Guid.NewGuid();
            userList.Add(user);
            return user;
        }

        public void Delete(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            //List<UserModel> userList = new List<UserModel>();
            
            return userList;
        }

        public UserModel GetById(Guid userId)
        {
            return GetAllUsers().FirstOrDefault(t => t.UserId == userId);
        }

        public UserModel Update(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
