using Moq;
using MyGameStat.Application.Repository;
using MyGameStat.Domain.Entity;
using MyGameStat.Web.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MyGameStat.Application.Extension;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using MyGameStat.Application.Service;
using MyGameStat.Application.DTO;

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

            _service.Setup(service => service.GetByUserIdAndFilter("1", Status.Owned, "Arcade", "PC")).Returns(new List<UserGame>() {
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
            var okResult = _controller.GetUserGames(Status.Owned, "Arcade", "PC") as OkObjectResult;
            _service.Verify(service => service.GetByUserIdAndFilter("1", Status.Owned, "Arcade", "PC"), Times.Once);

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

            _service.Setup(service => service.GetByUserIdAndFilter("1", Status.Owned, "Arcade", "PC")).Returns(new List<UserGame>() {
                
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
            var badResult = _controller.GetUserGames(Status.Owned, "Arcade", "PC") as NotFoundObjectResult;
            _service.Verify(service => service.GetByUserIdAndFilter("1", Status.Owned, "Arcade", "PC"), Times.Once);

            //  Assert
            Assert.NotNull(badResult);
            Assert.Equal((int)HttpStatusCode.NotFound, badResult.StatusCode);
        }

        [Fact]
        public void CreateUserGame_Valid() {
            //  Arrange
            var userId = "456-xyz";
            var userGameId = "abc-678";
            var noIdUserGameDto = new NoIdUserGameDto()
            {
                Status = Status.Owned,
                PlatformName = "PC",
                PlatformManufacturer = "PC",
                Title = "Space Invaders",
                Genre = "Arcade",
                ReleaseDate = new DateOnly(1978, 4, 1),
                Developer =  "Atari",
                Publisher = "Atari"
            };
            var userGame = noIdUserGameDto.ToModel();
            userGame.Id = userGameId;

            _service.Setup(service => service.Upsert(userId, It.IsAny<UserGame>())).Returns(userGame);

            var context = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.Name, ""),
                        new Claim(ClaimTypes.NameIdentifier, userId)
                    ], "mock")),
                }
            };

            var _controller = new UserGameController(_service.Object){
                ControllerContext = context
            };

            //  Act
            var okResult = _controller.CreateUserGame(noIdUserGameDto) as CreatedResult;

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
            var userId = "456-xyz";
            var userGameId = "abc-123";
            var updateStatus = Status.Wishlist;
            var noIdUserGameDto = new NoIdUserGameDto()
            {
                Status = updateStatus,
                PlatformName = "PC",
                PlatformManufacturer = "PC",
                Title = "Space Invaders",
                Genre = "Arcade",
                ReleaseDate = new DateOnly(1978, 4, 1),
                Developer =  "Atari",
                Publisher = "Atari"
            };
            var userGame = noIdUserGameDto.ToModel();

            _service.Setup(service => service.Update(userId, It.IsAny<UserGame>())).Returns(1);

            var context = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.Name, "Bob"),
                        new Claim(ClaimTypes.NameIdentifier, userId),
                    ], "mock")),
                }
            };

            var _controller = new UserGameController(_service.Object){
                ControllerContext = context
            };

            //  Update Act
            var okResult = _controller.UpdateUserGame(userGameId, noIdUserGameDto) as OkObjectResult;

            //  Assert
            Assert.NotNull(okResult);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
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
            var userId = "456-xyz";
            var userGameId = "abc-123";
            UserGame userGame = new()
            {
                Id = userGameId,
                CreatorId = userId,
                Game = new()
                {
                    CreatorId = userId,
                    Title = "Space Invaders",
                    Genre = "Arcade",
                    Developer = "Atari", 
                    Publisher = "Atari"
                },
                Platform = new()
                {
                    CreatorId = userId,
                    Name = "PC",
                    Manufacturer = "PC"
                },
                Status = Status.Owned,
            };

            _service.Setup(svc => svc.GetByUserIdAndFilter(userId, Status.Owned, "Arcade", "PC")).Returns([userGame]);
            _service.Setup(svc => svc.Delete(userGameId)).Returns(1);

            var context = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.Name, "Bob"),
                        new Claim(ClaimTypes.NameIdentifier, "1"),
                    ], "mock")),
                }
            };

            var _controller = new UserGameController(_service.Object){
                ControllerContext = context
            };

            //  Delete Act
            var okResult = _controller.DeleteUserGame(userGameId) as OkObjectResult;
            //_mockRepo.Verify(service => service.Delete("1"), Times.Once);

            //  Assert
            Assert.NotNull(okResult);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }
    }
}