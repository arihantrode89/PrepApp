using Entities;
using Entities.DTO;
using Entities.JwtEntity;
using Entities.LoginModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceContracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace PrepApi.Controllers
{
    [Route("api")]
    //[ServiceFilter(typeof(PrepApi.Filters.ActionFilter))]
    public class AuthenticationController : ControllerBase
    {
        //private readonly JwtSetting _jwtSetting;
        //private readonly ApplicationDbContext _context;
        private readonly IUserService _user;
        public AuthenticationController(IUserService user)
        {
           _user = user;
        }

        [HttpPost]
        [Route("token")]
        public IActionResult GenerateToken([FromBody] TokenUserDTO user)
        {
            var token = _user.GenerateUserToken(user);
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Username or password is invalid");
            }
            return Ok(token);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(UserDTO user)
        {
            var success = await _user.RegisterUser(user);
            if (!success)
            {
                return BadRequest("Username and Password is invalid");
            }
            return Ok();
        }

        [HttpGet]
        [Route("pallindrome")]
        public IActionResult Pallindrome(string Name)
        {
            var reverse = "";
            for(int i=Name.Length-1; i>=0;i--)
            {
                reverse += Name[i];
            }

            if (Name == reverse)
            {
                return Ok($"{Name} is Pallindrome");
            }
            else
            {
                return Ok($"{Name} is not Pallindrome");
            }
        }
    }
}
