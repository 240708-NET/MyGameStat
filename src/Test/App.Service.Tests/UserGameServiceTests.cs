using Moq;
using MyGameStat.Application.Repository;
using MyGameStat.Application.Service;
using MyGameStat.Domain.Entity;

namespace Test.App.Service.Tests {
    public class UserGameServiceTests {
        private readonly Mock<IGameRepository> _repoGame;
        private readonly Mock<IUserGameRepository> _repoUGame;
        private readonly Mock<IPlatformRepository> _repoPlatform;
        private readonly UserGameService _service;

        public UserGameServiceTests() {
            _repoGame = new Mock<IGameRepository>();
            _repoUGame = new Mock<IUserGameRepository>();
            _repoPlatform = new Mock<IPlatformRepository>();

            _service = new UserGameService(_repoUGame.Object, _repoGame.Object, _repoPlatform.Object);
        }

        [Fact]
        public void SanityCheck() {
            Assert.True(1 == 1);
        }

        [Fact]
        public void Upsert_Valid() {
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
                Id = "1",
                CreatorId = "1",
                Game = game,
                Platform = platform,
                Status = Status.Owned,
            };

            _repoGame.Setup(service => service.Retrieve(It.IsAny<Game>())).Returns(game);
            _repoPlatform.Setup(service => service.Retrieve(It.IsAny<Platform>())).Returns(platform);
            _repoUGame.Setup(service => service.Retrieve(It.IsAny<UserGame>())).Returns(userGame);
            _repoUGame.Setup(service => service.Save(It.IsAny<UserGame>())).Returns(userGame);

            //  Act
            var result = _service.Upsert("1", userGame);

            //  Assert
            Assert.NotNull(result);
            Assert.Equal("1", result.Id);
        }

        [Fact]
        public void Upsert_NullId() {
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
                Id = "1",
                CreatorId = "1",
                Game = game,
                Platform = platform,
                Status = Status.Owned,
            };

            _repoGame.Setup(service => service.Retrieve(It.IsAny<Game>())).Returns(game);
            _repoPlatform.Setup(service => service.Retrieve(It.IsAny<Platform>())).Returns(platform);
            _repoUGame.Setup(service => service.Retrieve(It.IsAny<UserGame>())).Returns(userGame);
            _repoUGame.Setup(service => service.Save(It.IsAny<UserGame>())).Returns(userGame);

            //  Act
            var result = _service.Upsert(null, userGame);

            //  Assert
            Assert.Null(result);
        }

        [Fact]
        public void Upsert_InvalidGame() {
            //  Arrange
            Game game1 = new Game() {
                CreatorId = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari", 
                Publisher = "Atari"
            };

            Game game2 = new Game() {
                CreatorId = "2",
                Title = "Doom",
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
                Id = "1",
                CreatorId = "1",
                Game = game1,
                Platform = platform,
                Status = Status.Owned,
            };

            _repoGame.Setup(service => service.Retrieve(It.IsAny<Game>())).Returns(game2);
            _repoPlatform.Setup(service => service.Retrieve(It.IsAny<Platform>())).Returns(platform);
            _repoUGame.Setup(service => service.Retrieve(It.IsAny<UserGame>())).Returns(userGame);
            _repoUGame.Setup(service => service.Save(It.IsAny<UserGame>())).Returns(userGame);

            //  Act
            var result = _service.Upsert("1", userGame);

            //  Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Upsert_InvalidPlatform() {
            //  Arrange
            Game game = new Game() {
                CreatorId = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari", 
                Publisher = "Atari"
            };

            Platform platform1 = new Platform() {
                CreatorId = "1",
                Name = "PC",
                Manufacturer = "PC"
            };

            Platform platform2 = new Platform() {
                CreatorId = "2",
                Name = "Xbox",
                Manufacturer = "Microsoft"
            };

            UserGame userGame = new UserGame() {
                Id = "1",
                CreatorId = "1",
                Game = game,
                Platform = platform1,
                Status = Status.Owned,
            };

            _repoGame.Setup(service => service.Retrieve(It.IsAny<Game>())).Returns(game);
            _repoPlatform.Setup(service => service.Retrieve(It.IsAny<Platform>())).Returns(platform2);
            _repoUGame.Setup(service => service.Retrieve(It.IsAny<UserGame>())).Returns(userGame);
            _repoUGame.Setup(service => service.Save(It.IsAny<UserGame>())).Returns(userGame);

            //  Act
            var result = _service.Upsert("1", userGame);

            //  Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Upsert_InvalidUGame() {
            //  Arrange
            Game game = new Game() {
                CreatorId = "1",
                Title = "Space Invaders",
                Genre = "Arcade",
                Developer = "Atari", 
                Publisher = "Atari"
            };

            Platform platform1 = new Platform() {
                CreatorId = "1",
                Name = "PC",
                Manufacturer = "PC"
            };

            Platform platform2 = new Platform() {
                CreatorId = "2",
                Name = "Xbox",
                Manufacturer = "Microsoft"
            };

            UserGame userGame1 = new UserGame() {
                Id = "1",
                CreatorId = "1",
                Game = game,
                Platform = platform1,
                Status = Status.Owned,
            };

            UserGame userGame2 = new UserGame() {
                Id = "2",
                CreatorId = "2",
                Game = game,
                Platform = platform2,
                Status = Status.Owned,
            };

            _repoGame.Setup(service => service.Retrieve(It.IsAny<Game>())).Returns(game);
            _repoPlatform.Setup(service => service.Retrieve(It.IsAny<Platform>())).Returns(platform1);
            _repoUGame.Setup(service => service.Retrieve(It.IsAny<UserGame>())).Returns(userGame2);
            _repoUGame.Setup(service => service.Save(It.IsAny<UserGame>())).Returns(userGame1);

            //  Act
            var result = _service.Upsert("1", userGame1);

            //  Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_Valid() {
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
                Id = "1",
                CreatorId = "1",
                Game = game,
                Platform = platform,
                Status = Status.Owned,
            };

            _repoGame.Setup(service => service.Retrieve(It.IsAny<Game>())).Returns(game);
            _repoPlatform.Setup(service => service.Retrieve(It.IsAny<Platform>())).Returns(platform);
            _repoUGame.Setup(service => service.Retrieve(It.IsAny<UserGame>())).Returns(userGame);
            _repoUGame.Setup(service => service.Save(It.IsAny<UserGame>())).Returns(userGame);
            _repoUGame.Setup(service => service.Delete(It.IsAny<string>()));

            //  Create Act
            var upsert = _service.Upsert("1", userGame);

            //  Create Assert
            Assert.NotNull(upsert);
            Assert.Equal("1", upsert.Id);

            //  Delete Act
            _service.Delete("1");

            //  Delete Assert
            _repoUGame.Verify(service => service.Delete("1"), Times.Once);
        }

        [Fact]
        public void GetByUserIdAndFilter_Valid() {
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
                Id = "1",
                CreatorId = "1",
                Game = game,
                Platform = platform,
                Status = Status.Owned,
            };

            _repoUGame.Setup(service => service.GetByUserIdAndFilter("1", Status.Owned, "Arcade", "PC")).Returns(new List<UserGame>() {
                userGame
            });

            //  Act
            var result = _service.GetByUserIdAndFilter("1", Status.Owned, "Arcade", "PC");

            //  Assert
            Assert.NotNull(result);
            Assert.Equal(userGame, result.ToList()[0]);
        }

        [Fact]
        public void Update_Valid() {
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
                Id = "1",
                CreatorId = "1",
                Game = game,
                Platform = platform,
                Status = Status.Owned,
            };

            _repoGame.Setup(service => service.Retrieve(It.IsAny<Game>())).Returns(game);
            _repoPlatform.Setup(service => service.Retrieve(It.IsAny<Platform>())).Returns(platform);
            _repoUGame.Setup(service => service.Retrieve(It.IsAny<UserGame>())).Returns(userGame);
            _repoUGame.Setup(service => service.Save(It.IsAny<UserGame>())).Returns(userGame);
            _repoUGame.Setup(service => service.Update(It.IsAny<UserGame>())).Returns(1);
            _repoUGame.Setup(service => service.GetById("1")).Returns(userGame);

            //  Create Act
            var upsert = _service.Upsert("1", userGame);

            //  Create Assert
            Assert.NotNull(upsert);
            Assert.Equal("1", upsert.Id);

            //  Update Act
            userGame.Status = Status.Completed;
            var result = _service.Update("1", userGame);
            _repoUGame.Verify(service => service.GetById("1"), Times.Once);

            //  Update Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Update_InvalidId() {
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
                Id = "1",
                CreatorId = "1",
                Game = game,
                Platform = platform,
                Status = Status.Owned,
            };

            _repoGame.Setup(service => service.Retrieve(It.IsAny<Game>())).Returns(game);
            _repoPlatform.Setup(service => service.Retrieve(It.IsAny<Platform>())).Returns(platform);
            _repoUGame.Setup(service => service.Retrieve(It.IsAny<UserGame>())).Returns(userGame);
            _repoUGame.Setup(service => service.Save(It.IsAny<UserGame>())).Returns(userGame);
            _repoUGame.Setup(service => service.Update(It.IsAny<UserGame>())).Returns(1);
            _repoUGame.Setup(service => service.GetById("1")).Returns(userGame);

            //  Create Act
            var upsert = _service.Upsert("1", userGame);

            //  Create Assert
            Assert.NotNull(upsert);
            Assert.Equal("1", upsert.Id);

            //  Update Act
            userGame.Status = Status.Completed;
            var result = _service.Update("", userGame);
            _repoUGame.Verify(service => service.GetById("1"), Times.Never);

            //  Update Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Update_InvalidUGame() {
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
                Id = "1",
                CreatorId = "1",
                Game = game,
                Platform = platform,
                Status = Status.Owned,
            };

            _repoGame.Setup(service => service.Retrieve(It.IsAny<Game>())).Returns(game);
            _repoPlatform.Setup(service => service.Retrieve(It.IsAny<Platform>())).Returns(platform);
            _repoUGame.Setup(service => service.Retrieve(It.IsAny<UserGame>())).Returns(userGame);
            _repoUGame.Setup(service => service.Save(It.IsAny<UserGame>())).Returns(userGame);
            _repoUGame.Setup(service => service.Update(It.IsAny<UserGame>())).Returns(1);

            //  Create Act
            var upsert = _service.Upsert("1", userGame);

            //  Create Assert
            Assert.NotNull(upsert);
            Assert.Equal("1", upsert.Id);

            //  Update Act
            userGame.Status = Status.Completed;
            var result = _service.Update("1", userGame);
            _repoUGame.Verify(service => service.GetById("1"), Times.Once);

            //  Update Assert
            Assert.Equal(0, result);
        }
    }
}
