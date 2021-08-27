using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiNetCoreProject2.Models;

namespace ApiNetCoreProject2
{
    // IUserManagerService contains the signatures of all methods seen in 
    // UserManagerService implementation.
    public interface IUserManagerService
    {
        UserModel Add(UserModel newUser);
        UserModel Update(Guid userId);
        IEnumerable<UserModel> GetAllUsers();
        UserModel GetById(Guid userId);
        void Delete(Guid userId);
    }
}
