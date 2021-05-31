using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using usermicroservice.Entities;
using UserService.Model;

namespace usermicroservice.Service
{
    public interface IUserservice
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task UpdateUser(int id, UserRequestDto user);
        Task<User> PostUser(UserRequestDto user);
        Task<bool> UserExists(int id);
        Task DeleteUser(int id);
    }
}
