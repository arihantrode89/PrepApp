using Entities;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using ServiceContracts;

namespace PrepApiTest
{
    public class AuthenticationControllerTest
    {
        [Fact]
        public async void RegisterUser_ShouldReturnOK()
        {
            //var dbContext = new Mock<ApplicationDbContext>();
            //var UserRepoMock = new Mock<UserRepository>(dbContext);
            var UserServiceMock = new Mock<IUserService>();
            var userObject = new UserDTO() { Email = "XYXX@gmail.com",Password="Amr@12345" ,Name="XYXB",Role="Admin"};
            UserServiceMock.Setup(setup => setup.RegisterUser(userObject)).ReturnsAsync(true);

            var authController = new AuthenticationController(UserServiceMock.Object);

            var result = await authController.RegisterUser(userObject);
            
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void GenerateToken_ShouldReturnBadRequest()
        {
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(stp => stp.GenerateUserToken(It.IsAny<TokenUserDTO>())).Returns("");

            var authController = new AuthenticationController(userServiceMock.Object);
            
            var result = authController.GenerateToken(It.IsAny<TokenUserDTO>());
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}