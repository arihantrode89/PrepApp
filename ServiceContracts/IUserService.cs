using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IUserService
    {
        public Task<bool> RegisterUser(UserDTO user);

        public string GenerateUserToken(TokenUserDTO user);
    }
}
