using Moq;
using MyGameStat.Application.Repository;
using MyGameStat.Domain.Entity;
using MyGameStat.Web.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MyGameStat.Application.DTO;
using MyGameStat.Application.Extension;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Security.Claims;
using MyGameStat.Application.Service;

namespace Test.API.Controllers.Tests {
    public class UserGameControllerTests {
        private readonly Mock<IGameRepository> _repoGame;
        private readonly Mock<IUserGameRepository> _repoUGame;
        private readonly Mock<IPlatformRepository> _repoPlatform;
        private readonly Mock<IUserGameService<UserGame, string>> _service;

        public UserGameControllerTests() {
            _repoGame = new Mock<IGameRepository>();
            _repoUGame = new Mock<IUserGameRepository>();
            _repoPlatform = new Mock<IPlatformRepository>();

            _service = new Mock<IUserGameService<UserGame, string>>();
        }

        [Fact]
        public void SanityCheck() {
            Assert.True(1 == 1);
        }

        [Fact]
        public void GetUserGames_Valid() {
            //  Arrange
            Game game1 = new Game() {
                CreatorId = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari", 
                Publisher = "Atari"
            };

            Platform platform = new Platform() {
                CreatorId = "1",
                Name = "PC",
                Manufacturer = "PC"
            };

            _service.Setup(service => service.GetByUserId("1")).Returns(new List<UserGame>() {
                new UserGame() { CreatorId = "1", Game = game1, Platform = platform, Status = Status.Owned }
            });

            var context = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, ""),
                        new Claim(ClaimTypes.NameIdentifier, "1"),
                    }, "mock")),
                }
            };

            var _controller = new UserGameController(_service.Object) {
                ControllerContext = context
            };
            
            //  Act
            var okResult = _controller.GetUserGames() as OkObjectResult;
            _service.Verify(service => service.GetByUserId("1"), Times.Once);

            //  Assert
            Assert.NotNull(okResult);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public void GetUserGames_Invalid() {
            //  Arrange
            Game game1 = new Game() {
                CreatorId = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari", 
                Publisher = "Atari"
            };

            Platform platform = new Platform() {
                CreatorId = "1",
                Name = "PC",
                Manufacturer = "PC"
            };

            _service.Setup(service => service.GetByUserId("1")).Returns(new List<UserGame>() {
                
            });

            var context = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, ""),
                        new Claim(ClaimTypes.NameIdentifier, "1"),
                    }, "mock")),
                }
            };

            var _controller = new UserGameController(_service.Object) {
                ControllerContext = context
            };
            
            //  Act
            var badResult = _controller.GetUserGames() as NotFoundObjectResult;
            _service.Verify(service => service.GetByUserId("1"), Times.Once);

            //  Assert
            Assert.NotNull(badResult);
            Assert.Equal((int)HttpStatusCode.NotFound, badResult.StatusCode);
        }

        [Fact]
        public void CreateUserGame_Valid() {
            //  Arrange
            Game game = new Game() {
                CreatorId = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari", 
                Publisher = "Atari"
            };

            Platform platform = new Platform() {
                CreatorId = "1",
                Name = "PC",
                Manufacturer = "PC"
            };

            UserGame userGame = new UserGame() {
                CreatorId = "1",
                Game = game,
                Platform = platform,
                Status = Status.Owned,
            };

            _service.Setup(service => service.Upsert("1", It.IsAny<UserGame>())).Returns("1");

            var context = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, ""),
                        new Claim(ClaimTypes.NameIdentifier, "1"),
                    }, "mock")),
                }
            };

            var _controller = new UserGameController(_service.Object){
                ControllerContext = context
            };

            //  Act
            var okResult = _controller.CreateUserGame(userGame.ToDto()) as CreatedResult;

            //  Assert
            Assert.NotNull(okResult);
        }

        //  CreateUserGame_Invalid
        /*
        [Fact]
        public async Task CreateUserGame_Invalid() {
            //  Arrange
            Game game = new Game() {
                CreatorId = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari", 
                Publisher = "Atari"
            };

            Platform platform = new Platform() {
                CreatorId = "1",
                Name = "PC",
                Manufacturer = "PC"
            };

            UserGame userGame = new UserGame() {
                CreatorId = "1",
                Game = game,
                Platform = platform,
                Status = Status.Owned,
            };

            _service.Setup(service => service.Upsert("1", It.IsAny<UserGame>())).Returns("1");

            var context = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, ""),
                        new Claim(ClaimTypes.NameIdentifier, "1"),
                    }, "mock")),
                }
            };

            var _controller = new UserGameController(_service.Object){
                //ControllerContext = context
            };

            //  Act
            var badResult = _controller.CreateUserGame(userGame.ToDto()) as UnprocessableEntityResult;

            //  Assert
            Assert.NotNull(badResult);
        }
        */

        [Fact]
        public void UpdateGame_Valid() {
            //  Arrange
            Game game = new Game() {
                CreatorId = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari", 
                Publisher = "Atari"
            };

            Platform platform = new Platform() {
                CreatorId = "1",
                Name = "PC",
                Manufacturer = "PC"
            };

            UserGame userGame = new UserGame() {
                CreatorId = "1",
                Game = game,
                Platform = platform,
                Status = Status.Owned,
            };

            _service.Setup(service => service.Upsert("1", It.IsAny<UserGame>())).Returns("1");
            _service.Setup(service => service.Update("1", It.IsAny<UserGame>())).Returns(1);

            var context = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, "Bob"),
                        new Claim(ClaimTypes.NameIdentifier, "1"),
                    }, "mock")),
                }
            };

            var _controller = new UserGameController(_service.Object){
                ControllerContext = context
            };

            var okResult1 = _controller.CreateUserGame(userGame.ToDto()) as CreatedResult;

            //  Create Assert
            Assert.NotNull(okResult1);

            //  Update Act
            userGame.Status = Status.Wishlist;

            var okResult2 = _controller.UpdateUserGame("1", userGame.ToDto()) as OkResult;

            //  Assert
            Assert.NotNull(okResult2);
            Assert.Equal((int)HttpStatusCode.OK, okResult2.StatusCode);
        }

        //  UpdateGame_Invalid
        /*
        [Fact]
        public async Task UpdateGame_Invalid() {
            //  Arrange
            Game game = new Game() {
                CreatorId = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari", 
                Publisher = "Atari"
            };

            Platform platform = new Platform() {
                CreatorId = "1",
                Name = "PC",
                Manufacturer = "PC"
            };

            UserGame userGame = new UserGame() {
                CreatorId = "1",
                Game = game,
                Platform = platform,
                Status = Status.Owned,
            };

            _service.Setup(service => service.Upsert("1", It.IsAny<UserGame>())).Returns("1");

            var context = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, ""),
                        new Claim(ClaimTypes.NameIdentifier, "1"),
                    }, "mock")),
                }
            };

            var _controller = new UserGameController(_service.Object){
                ControllerContext = context
            };

            //  Act
            var badResult = _controller.UpdateUserGame("1", userGame.ToDto()) as UnprocessableEntityResult;

            //  Assert
            Assert.NotNull(badResult);
            Assert.Equal((int)HttpStatusCode.UnprocessableEntity, badResult.StatusCode);
        }
        */

        [Fact]
        public void DeleteGame_Valid() {
            //  Arrange
            Game game = new Game() {
                CreatorId = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari", 
                Publisher = "Atari"
            };

            Platform platform = new Platform() {
                CreatorId = "1",
                Name = "PC",
                Manufacturer = "PC"
            };

            UserGame userGame = new UserGame() {
                CreatorId = "1",
                Game = game,
                Platform = platform,
                Status = Status.Owned,
            };

            _service.Setup(service => service.GetByUserId("1")).Returns(new List<UserGame>() {
                userGame
            });
            _service.Setup(service => service.Upsert("1", It.IsAny<UserGame>())).Returns("1");
            _service.Setup(service => service.Update("1", It.IsAny<UserGame>())).Returns(1);
            _service.Setup(service => service.Delete(It.IsAny<string>()));

            var context = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, "Bob"),
                        new Claim(ClaimTypes.NameIdentifier, "1"),
                    }, "mock")),
                }
            };

            var _controller = new UserGameController(_service.Object){
                ControllerContext = context
            };
            
            //  Create Act
            var okResult1 = _controller.CreateUserGame(userGame.ToDto()) as CreatedResult;
            //_mockRepo.Verify(service => service.Create(game), Times.Once);

            //  Create Assert
            Assert.NotNull(okResult1);
            
            //  Get Act
            var okResult2 = _controller.GetUserGames() as OkObjectResult;
            _service.Verify(service => service.GetByUserId("1"), Times.Once);

            Assert.NotNull(okResult2);
            Assert.Equal((int)HttpStatusCode.OK, okResult2.StatusCode);

            //  Delete Act
            var okResult3 = _controller.DeleteUserGame("1") as OkResult;
            //_mockRepo.Verify(service => service.Delete("1"), Times.Once);

            //  Assert
            Assert.NotNull(okResult3);
            Assert.Equal((int)HttpStatusCode.OK, okResult3.StatusCode);
        }
    }
}