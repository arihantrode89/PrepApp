using Entities.DTO;
using Entities.LoginModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IUserRepository
    {
        public UserModel IsValidUser(TokenUserDTO user);

        public Task<bool> RegisterSystemUser(UserDTO user);

        public UserModel GetUserData(int id);

    }
}
