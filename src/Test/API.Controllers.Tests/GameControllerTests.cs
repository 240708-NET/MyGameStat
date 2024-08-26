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

namespace Test.API.Controllers.Tests {
    public class GameControllerTests {
        private readonly Mock<IGameRepository> _mockRepo;
        private readonly GameController _controller;

        public GameControllerTests() {
            _mockRepo = new Mock<IGameRepository>();
            _controller = new GameController(_mockRepo.Object);
        }

        [Fact]
        public void SanityCheck() {
            Assert.True(1 == 1);
        }

        [Fact]
        public async Task GetById_Valid() {
            //  Arrange
            _mockRepo.Setup(service => service.GetById("1")).Returns(Task.FromResult(new Game() {
                Id = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari",
                Publisher = "Atari"
            } ?? null));
            
            //  Act
            var okResult = (await _controller.GetById("1")) as OkObjectResult;
            _mockRepo.Verify(service => service.GetById("1"), Times.Once);

            //  Assert
            Assert.NotNull(okResult);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);

            var result = okResult.Value as GameDto;
            Assert.NotNull(result);
            Assert.Equal("1", result.Id);
        }

        [Fact]
        public async Task GetById_Invalid() {
            //  Arrange
            _mockRepo.Setup(service => service.GetById("1")).Returns(Task.FromResult(new Game() {
                Id = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari",
                Publisher = "Atari"
            } ?? null));
            
            //  Act
            var badResult = (await _controller.GetById("2")) as NotFoundObjectResult;
            _mockRepo.Verify(service => service.GetById("2"), Times.Once);

            //  Assert
            Assert.NotNull(badResult);
            Assert.Equal((int)HttpStatusCode.NotFound, badResult.StatusCode);
        }

        [Fact]
        public async Task GetGamesByTitle_Valid() {
            //  Arrange
            _mockRepo.Setup(service => service.GetGamesByTitle("Space Invaders")).Returns(Task.FromResult(new List<Game>() {
                new Game() { Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari"},
                new Game() { Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari"},
                new Game() { Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari"},
            }));

            //  Act
            var okResult = (await _controller.GetGames("Space Invaders")) as OkObjectResult;
            _mockRepo.Verify(service => service.GetGamesByTitle("Space Invaders"), Times.Once);

            //  Assert
            Assert.NotNull(okResult);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);

            var result = okResult.Value as List<GameDto>;
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetGamesByTitle_Invalid() {
            //  Arrange
            _mockRepo.Setup(service => service.GetGamesByTitle("Space Invaders")).Returns(Task.FromResult(new List<Game>() {
                new Game() { Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari"},
                new Game() { Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari"},
                new Game() { Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari"},
            }));

            //  Act
            var badResult = (await _controller.GetGames("Doom")) as NotFoundObjectResult;
            _mockRepo.Verify(service => service.GetGamesByTitle("Doom"), Times.Once);

            //  Assert
            Assert.NotNull(badResult);
            Assert.Equal((int)HttpStatusCode.NotFound, badResult.StatusCode);
        }

        [Fact]
        public async Task GetGamesByTitle_NullFound() {
            //  Arrange
            _mockRepo.Setup(service => service.GetAll()).Returns(Task.FromResult(new List<Game>() {
                new Game() { Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari"},
                new Game() { Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari"},
                new Game() { Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari"},
            }));

            //  Act
            var okResult = (await _controller.GetGames(null)) as OkObjectResult;
            _mockRepo.Verify(service => service.GetAll(), Times.Once);

            //  Assert
            Assert.NotNull(okResult);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetByUserName_NullNotFound() {
            //  Arrange

            //  Act
            var badResult = (await _controller.GetGames(null)) as NotFoundObjectResult;
            _mockRepo.Verify(service => service.GetAll(), Times.Once);

            //  Assert
            Assert.NotNull(badResult);
            Assert.Equal((int)HttpStatusCode.NotFound, badResult.StatusCode);
            Assert.Equal("No games found", badResult.Value);
        }

        [Fact]
        public async Task CreateGame_Valid() {
            //  Arrange
            var context = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, "Bob"),
                        new Claim(ClaimTypes.NameIdentifier, "1"),
                    }, "mock")),
                }
            };

            _controller.ControllerContext = context;
            _mockRepo.Name = "Dave";

            _mockRepo.Setup(service => service.Create(It.IsAny<Game>())).Returns(Task.FromResult(1));
            
            //  Act
            var game = new Game() {
                Id = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari",
                Publisher = "Atari"
            };

            var okResult = (await _controller.CreateGame(game.ToDto())) as CreatedResult;
            _mockRepo.Verify(service => service.Create(game), Times.Once);

            //  Assert
            Assert.NotNull(okResult);
        }

        [Fact]
        public async Task CreateGame_Invalid() {
            //  Arrange
            var context = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, ""),
                        new Claim(ClaimTypes.NameIdentifier, "1"),
                    }, "mock")),
                }
            };

            _controller.ControllerContext = context;
            _mockRepo.Name = "";

            _mockRepo.Setup(service => service.Create(It.IsAny<Game>())).Returns(Task.FromResult(1));
            
            //  Act
            var game = new Game() {
                Id = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari",
                Publisher = "Atari"
            };

            var badResult = (await _controller.CreateGame(game.ToDto())) as UnprocessableEntityResult;
            _mockRepo.Verify(service => service.Create(game), Times.Never);

            //  Assert
            Assert.NotNull(badResult);
            Assert.Equal((int)HttpStatusCode.UnprocessableEntity, badResult.StatusCode);
        }

        [Fact]
        public async Task UpdateGame_Valid() {
            //  Arrange
            var context = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, "Bob"),
                        new Claim(ClaimTypes.NameIdentifier, "1"),
                    }, "mock")),
                }
            };

            _controller.ControllerContext = context;
            _mockRepo.Name = "Dave";

            _mockRepo.Setup(service => service.Create(It.IsAny<Game>())).Returns(Task.FromResult(1));
            _mockRepo.Setup(service => service.Update(It.IsAny<Game>())).Returns(Task.FromResult(1));
            
            //  Create Act
            var game = new Game() {
                Id = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari",
                Publisher = "Atari"
            };

            var okResult1 = (await _controller.CreateGame(game.ToDto())) as CreatedResult;
            Debug.WriteLine(_mockRepo.Invocations[0]);
            Debug.WriteLine(_mockRepo.Invocations.Count);
            _mockRepo.Verify(service => service.Create(game), Times.Once);

            //  Create Assert
            Assert.NotNull(okResult1);

            //  Update Act
            game.Title = "Doom";

            var okResult2 = (await _controller.UpdateGame(game.ToDto())) as OkResult;
            _mockRepo.Verify(service => service.Update(game), Times.Once);

            //  Assert
            Assert.NotNull(okResult2);
            Assert.Equal((int)HttpStatusCode.OK, okResult2.StatusCode);
        }

        [Fact]
        public async Task UpdateGame_Invalid() {
            //  Arrange
            var context = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, ""),
                        new Claim(ClaimTypes.NameIdentifier, "1"),
                    }, "mock")),
                }
            };

            _controller.ControllerContext = context;
            _mockRepo.Name = "";

            _mockRepo.Setup(service => service.Update(It.IsAny<Game>())).Returns(Task.FromResult(1));
            
            //  Act
            var game = new Game() {
                Id = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari",
                Publisher = "Atari"
            };

            var badResult = (await _controller.UpdateGame(game.ToDto())) as UnprocessableEntityResult;
            _mockRepo.Verify(service => service.Update(game), Times.Never);

            //  Assert
            Assert.NotNull(badResult);
            Assert.Equal((int)HttpStatusCode.UnprocessableEntity, badResult.StatusCode);
        }
    
        [Fact]
        public async Task DeleteGame_Valid() {
            //  Arrange
            var context = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, "Bob"),
                        new Claim(ClaimTypes.NameIdentifier, "1"),
                    }, "mock")),
                }
            };

            _controller.ControllerContext = context;
            _mockRepo.Name = "Dave";

            var game = new Game() {
                Id = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari",
                Publisher = "Atari"
            };

            _mockRepo.Setup(service => service.Create(It.IsAny<Game>())).Returns(Task.FromResult(1));
            _mockRepo.Setup(service => service.Delete(It.IsAny<string>()));
            _mockRepo.Setup(service => service.GetById("1")).Returns(Task.FromResult(game ?? null));
            
            //  Create Act
            var okResult1 = (await _controller.CreateGame(game.ToDto())) as CreatedResult;
            _mockRepo.Verify(service => service.Create(game), Times.Once);

            //  Create Assert
            Assert.NotNull(okResult1);
            
            //  Get Act
            var okResult2 = (await _controller.GetById("1")) as OkObjectResult;
            _mockRepo.Verify(service => service.GetById("1"), Times.Once);

            Assert.NotNull(okResult2);
            Assert.Equal((int)HttpStatusCode.OK, okResult2.StatusCode);

            //  Delete Act
            var okResult3 = (await _controller.DeleteGame("1")) as OkObjectResult;
            _mockRepo.Verify(service => service.Delete("1"), Times.Once);

            //  Assert
            Assert.NotNull(okResult3);
            Assert.Equal((int)HttpStatusCode.OK, okResult3.StatusCode);
        }

        [Fact]
        public async Task DeleteGame_Invalid() {
            //  Arrange
            var context = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, "Bob"),
                        new Claim(ClaimTypes.NameIdentifier, "1"),
                    }, "mock")),
                }
            };

            _controller.ControllerContext = context;
            _mockRepo.Name = "Dave";

            var game = new Game() {
                Id = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari",
                Publisher = "Atari"
            };

            _mockRepo.Setup(service => service.Create(It.IsAny<Game>())).Returns(Task.FromResult(1));
            _mockRepo.Setup(service => service.Delete(It.IsAny<string>()));
            _mockRepo.Setup(service => service.GetById("1")).Returns(Task.FromResult(game ?? null));
            
            //  Create Act
            var okResult1 = (await _controller.CreateGame(game.ToDto())) as CreatedResult;
            _mockRepo.Verify(service => service.Create(game), Times.Once);

            //  Create Assert
            Assert.NotNull(okResult1);
            
            //  Get Act
            var okResult2 = (await _controller.GetById("1")) as OkObjectResult;
            _mockRepo.Verify(service => service.GetById("1"), Times.Once);

            Assert.NotNull(okResult2);
            Assert.Equal((int)HttpStatusCode.OK, okResult2.StatusCode);

            //  Delete Act
            //var okResult3 = (await _controller.DeleteGame("2"));
            //_mockRepo.Verify(service => service.Delete("2"), Times.Once);

            //  Assert
            //Assert.Null(okResult3);
        }
    }
}