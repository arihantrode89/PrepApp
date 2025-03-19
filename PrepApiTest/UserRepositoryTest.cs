using Entities;
using Entities.DTO;
using Entities.LoginModel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepApiTest
{
    public class UserRepositoryTest
    {
        [Fact]
        public void IsValidUser_ReturnUserModelObject()
        {
            var Roles = new List<Roles>()
            {
               new Roles(){ Id=1,RoleName="Admin" },

            }.AsQueryable();

            var userModel = new List<UserModel>()
            {
               new UserModel(){ Name = "Arihant Rode",Id=1,Email="arihantrode89@gmail.com",RoleId=1,Role=Roles.SingleOrDefault(x=>x.Id==1) },
               new UserModel(){ Name = "Aditya Patil",Id=1,Email="aditya@gmail.com",RoleId=1,Role=Roles.SingleOrDefault(x=>x.Id==1) },

            }.AsQueryable();
            
            var userDbSet = new Mock<DbSet<UserModel>>();
            var roleDbSet = new Mock<DbSet<Roles>>();
            var mockDb = new Mock<ApplicationDbContext>();
            mockDb.As<IQueryable<UserModel>>().Setup(x => x.Provider).Returns(userModel.Provider);
            mockDb.As<IQueryable<UserModel>>().Setup(x => x.Expression).Returns(userModel.Expression);
            mockDb.As<IQueryable<UserModel>>().Setup(x => x.ElementType).Returns(userModel.ElementType);
            mockDb.As<IQueryable<UserModel>>().Setup(x => x.GetEnumerator()).Returns(userModel.GetEnumerator());

            mockDb.As<IQueryable<Roles>>().Setup(x => x.Provider).Returns(Roles.Provider);
            mockDb.As<IQueryable<Roles>>().Setup(x => x.Expression).Returns(Roles.Expression);
            mockDb.As<IQueryable<Roles>>().Setup(x => x.ElementType).Returns(Roles.ElementType);
            mockDb.As<IQueryable<Roles>>().Setup(x => x.GetEnumerator()).Returns(Roles.GetEnumerator());

            mockDb.Setup(x=>x.users).Returns(userDbSet.Object);
            mockDb.Setup(x=>x.roles).Returns(roleDbSet.Object);

            var userRepo = new UserRepository(mockDb.Object);
            var tokenObj = new TokenUserDTO() { UserName="arihantrode89@gmail.com",Password="Amr@12345"};

            var result = userRepo.IsValidUser(tokenObj);

            Assert.IsType<UserModel>(result);
        }

        [Fact]
        public void IsValidUser_ReturnUserModelObject1()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Server=BLR1-LHP-N80812;Database=PrepDB;Trusted_Connection=True;TrustServerCertificate=True;").Options;


            var dbContext = new ApplicationDbContext(dbOptions);

            var userRepo = new UserRepository(dbContext);
            var tokenObj = new TokenUserDTO() { UserName = "arihantrode89@gmail.com", Password = "Amr@12345" };
            var result = userRepo.IsValidUser(tokenObj);


            Assert.IsType<UserModel>(result);
            Assert.Equal("Admin", result.Role.RoleName);
            
        }
    }
}
