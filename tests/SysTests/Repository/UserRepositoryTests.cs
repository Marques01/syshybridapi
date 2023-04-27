using BLL.Models;
using BLL.Repository.Interfaces;
using DAL.Context;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SysTests.Repository
{
    public class UserRepositoryTests
    {
        private IUnitOfWork _uof;

        public UserRepositoryTests()
        {
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=syspdv_dsv;User Id=sa;Password=Binario010101!;";

            var service = new ServiceCollection();

            service.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

            service.AddScoped<IUnitOfWork, UnitOfWork>();

            var provider = service.BuildServiceProvider();

            _uof = provider.GetRequiredService<IUnitOfWork>();
        }

        [Fact]
        public async void CreateUserAsync()
        {
            // Asert

            // Act
            User user = new()
            {
                CreateAt = DateTime.Now,
                Login = "email@email.com",
                Enabled = true,
                FailedCount = 0,
                FirstName = "Test",
                LastLogin = DateTime.Now,
                LastName = "Test2",
                Password = Guid.NewGuid().ToString(),
            };

            await _uof.UserRepository.CreateAsync(user);

            bool sucess = await _uof.CommitAsync();

            // Assert

            Assert.True(sucess);
        }

        [Fact]
        public async void CheckUserExistsAsync()
        {
            //Assert

            //Act

            User user = new()
            {
                CreateAt = DateTime.Now,
                Login = "email@email.com",
                Enabled = true,
                FailedCount = 0,
                FirstName = "Test",
                LastLogin = DateTime.Now,
                LastName = "Test2",
                Password = "5145cd13-fb63-4e7c-a1b3-88a2f83e4d0e",
            };

            bool userExists = await _uof.UserRepository.UserExistingAsync(user);

            // Assert

            Assert.True(userExists);
        }

        [Fact]
        public async void CheckUserNotExistsAsync()
        {
            //Assert

            //Act

            User user = new()
            {
                CreateAt = DateTime.Now,
                Login = "email@email.com",
                Enabled = true,
                FailedCount = 0,
                FirstName = "Test",
                LastLogin = DateTime.Now,
                LastName = "Test2",
                Password = "6145cd13-fb63-4e7c-a1b3-88a2f83e4d0e",
            };

            bool userExists = await _uof.UserRepository.UserExistingAsync(user);

            // Assert

            Assert.False(userExists);
        }
    }

}