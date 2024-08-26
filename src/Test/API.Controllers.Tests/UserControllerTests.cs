using Moq;
using MyGameStat.Application.Repository;
using MyGameStat.Web.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using MyGameStat.Domain.Entity;
using System.Net;
using System.Diagnostics;

namespace Test.API.Controllers.Tests {
    public class UserControllerTests {
        private readonly Mock<IUserRepository<string>> _mockRepo;
        private readonly UserController _controller;

        public UserControllerTests() {
            _mockRepo = new Mock<IUserRepository<string>>();
            _controller = new UserController(_mockRepo.Object);
        }

        [Fact]
        public void SanityCheck() {
            Assert.True(1 == 1);
        }

        [Fact]
        public async Task GetById_Valid() {
            //  Arrange
            _mockRepo.Setup(service => service.GetById("1")).Returns(Task.FromResult(new User() {
                Id = "1",
            } ?? null));
            
            //  Act
            var okResult = (await _controller.GetById("1")) as OkObjectResult;
            _mockRepo.Verify(service => service.GetById("1"), Times.Once);

            //  Assert
            Assert.NotNull(okResult);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);

            var result = okResult.Value as User;
            Assert.NotNull(result);
            Assert.Equal("1", result.Id);
        }

        [Fact]
        public async Task GetById_Invalid() {
            //  Arrange
            _mockRepo.Setup(service => service.GetById("1")).Returns(Task.FromResult(new User() {
                Id = "1",
            } ?? null));
            
            //  Act
            var badResult = (await _controller.GetById("2")) as NotFoundObjectResult;
            _mockRepo.Verify(service => service.GetById("2"), Times.Once);

            //  Assert
            Assert.NotNull(badResult);
            Assert.Equal((int)HttpStatusCode.NotFound, badResult.StatusCode);
        }

        [Fact]
        public async Task GetByUserName_Valid() {
            //  Arrange
            _mockRepo.Setup(service => service.GetByUserName("Dave")).Returns(Task.FromResult(new User() {
                Id = "1",
                UserName = "Dave",
            } ?? null));
            
            //  Act
            var okResult = (await _controller.GetAllUsers("Dave")) as OkObjectResult;
            _mockRepo.Verify(service => service.GetByUserName("Dave"), Times.Once);

            //  Assert
            Assert.NotNull(okResult);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);

            var result = okResult.Value as User;
            Assert.NotNull(result);
            Assert.Equal("Dave", result.UserName);
        }

        [Fact]
        public async Task GetByUserName_Invalid() {
            //  Arrange
            _mockRepo.Setup(service => service.GetByUserName("Dave")).Returns(Task.FromResult(new User() {
                Id = "1",
                UserName = "Dave",
            } ?? null));

            //  Act
            var badResult = (await _controller.GetAllUsers("Steve")) as NotFoundObjectResult;
            _mockRepo.Verify(service => service.GetByUserName("Steve"), Times.Once);

            //  Assert
            Assert.NotNull(badResult);
            Assert.Equal((int)HttpStatusCode.NotFound, badResult.StatusCode);
        }

        [Fact]
        public async Task GetByUserName_NullFound() {
            //  Arrange
            _mockRepo.Setup(service => service.GetAll()).Returns(Task.FromResult(new List<User>() {
                new User() { Id = "1", UserName = "Dave" },
            }));

            //  Act
            var okResult = (await _controller.GetAllUsers(null)) as OkObjectResult;
            _mockRepo.Verify(service => service.GetAll(), Times.Once);

            //  Assert
            Assert.NotNull(okResult);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetByUserName_NullNotFound() {
            //  Arrange

            //  Act
            var badResult = (await _controller.GetAllUsers(null)) as NotFoundObjectResult;
            _mockRepo.Verify(service => service.GetAll(), Times.Once);

            //  Assert
            Assert.NotNull(badResult);
            Assert.Equal((int)HttpStatusCode.NotFound, badResult.StatusCode);
            Assert.Equal("No users found", badResult.Value);
        }
    }
}