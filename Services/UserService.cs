using Entities.DTO;
using Entities.JwtEntity;
using Microsoft.IdentityModel.Tokens;
using RepositoryContracts;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly JwtSetting _jwt;
        public UserService(IUserRepository user,JwtSetting jwt)
        {
            _userRepo = user;
            _jwt = jwt;
        }
        public string GenerateUserToken(TokenUserDTO user)
        {
            var userData = _userRepo.IsValidUser(user);
            if (userData.Id !=-1)
            {
                //var userData = _userRepo.GetUserData(user.)
                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role,userData.Role.RoleName),

                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));
                var hash = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(issuer:_jwt.Issuer,audience:_jwt.Audience,expires:DateTime.Now.AddMinutes(_jwt.ExpirationMinutes),signingCredentials:hash,claims:claims);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                return "";
            }
        }

        public async Task<bool> RegisterUser(UserDTO user)
        {
            var sucess = await _userRepo.RegisterSystemUser(user);
            return sucess;
        }
    }
}
