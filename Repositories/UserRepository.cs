using Entities;
using Entities.DTO;
using Entities.LoginModel;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext db)
        {
            _context = db;
        }

        public UserModel GetUserData(int id)
        {
            var data = _context.users.Include(x => x.Role).SingleOrDefault(x => x.Id == id);
            if (data == null)
            {
                return new UserModel();
            }
            return data;
        }

        public UserModel IsValidUser(TokenUserDTO user)
        {
            var dbUser = _context.users.Include(x => x.Role).SingleOrDefault(x => x.Email == user.UserName);
            if (dbUser == null)
            {
                return new UserModel() { Id=-1};
            }
            var password = _context.passwords.SingleOrDefault(x => x.UserId == dbUser.Id);
            if (password == null)
            {
                return new UserModel() { Id = -1 };
            }
            if (password.PasswordHash != user.Password)
            {
                return new UserModel() { Id = -1 };
            }
            return dbUser;
        }

        public async Task<bool> RegisterSystemUser(UserDTO user)
        {
            var role = _context.roles.FirstOrDefault(x => x.RoleName == user.Role);
            if (role == null)
            {
                return false;
            }
            var password = new Password() { PasswordHash = user.Password };
            var userData = new UserModel() { Name = user.Name, Email = user.Email, Password = password, Role = role };
            await _context.users.AddAsync(userData);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
