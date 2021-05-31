using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using usermicroservice.Entities;
using usermicroservice.Utilities;
using UserService.Database;
using UserService.Model;

namespace usermicroservice.Service
{
    public class UserService : IUserservice
    {
        private readonly DataBaseContext _context;
        private readonly IErrorHelper _errorHelper;
        public UserService(DataBaseContext context, IErrorHelper errorHelper)
        {
            _context = context;
            _errorHelper = errorHelper;
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> PostUser(UserRequestDto userRequestDto)
        {
            User user = new User();
            user.UserId = userRequestDto.Id;
            user.Name = userRequestDto.Name;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            //get user by id
            return await GetLastestUser();
        }

        public async Task UpdateUser(int id, UserRequestDto userRequestDto)
        {
            if (id != userRequestDto.Id)
            {
                //return BadRequest();
                _errorHelper.HandleError("Bad Request");
            }

            User user = new User();
            user.UserId = userRequestDto.Id;
            user.Name = userRequestDto.Name;
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await UserExists(id)))
                {
                    _errorHelper.HandleError("User not found", System.Net.HttpStatusCode.NotFound);
                    //return NotFound();
                }
                else
                {
                    throw ;
                }
            }
        }

        public async Task<bool> UserExists(int id)
        {
            return await _context.Users.AnyAsync(e => e.UserId == id);
        }
        public async Task<User> GetLastestUser()
        {
            return await _context.Users.OrderByDescending(x => x.UserId).LastOrDefaultAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                //return NotFound();
                _errorHelper.HandleError("User Not Found", System.Net.HttpStatusCode.NotFound);
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
