using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MyGameStat.Domain.Entity;
using MyGameStat.Infrastructure.Persistence;
using MyGameStat.Infrastructure.Repository;

namespace Test.API.Repository.Tests {
    public class UserGameRepositoryTests {
        private DbContextOptions<ApplicationDbContext>? _options;

        [Fact]
        public void SanityCheck() {
            Assert.True(1 == 1);
        }

        [Fact]
        public void GetById_Valid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(new UserGame { Id = "1", CreatorId = "1", User = new User(), Status = Status.Owned,
                    Platform = new Platform() { CreatorId = "1", Name = "PC", Manufacturer = "PC" }, 
                    Game = new Game() { CreatorId = "1", Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari" },  });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                UserGameRepository repo = new UserGameRepository(context);
                UserGame? uGame = repo.GetById("1");

                //  Assert
                Assert.NotNull(uGame);
            }
        }

        [Fact]
        public void GetById_Invalid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(new UserGame { Id = "1", CreatorId = "1", User = new User(), Status = Status.Owned,
                    Platform = new Platform() { CreatorId = "1", Name = "PC", Manufacturer = "PC" }, 
                    Game = new Game() { CreatorId = "1", Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari" },  });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                UserGameRepository repo = new UserGameRepository(context);
                UserGame? uGame = repo.GetById("2");

                //  Assert
                Assert.Null(uGame);
            }
        }

        [Fact]
        public void Retrieve_Valid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var userGame = new UserGame { Id = "1", CreatorId = "1", User = new User(), Status = Status.Owned,
                    Platform = new Platform() { Id = "1", CreatorId = "1", Name = "PC", Manufacturer = "PC" }, 
                    Game = new Game() { Id = "1", CreatorId = "1", Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari" }};

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(userGame);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                UserGameRepository repo = new UserGameRepository(context);
                UserGame? uGame = repo.Retrieve(userGame);

                //  Assert
                Assert.NotNull(uGame);
            }
        }

        [Fact]
        public void GetByUserIdAndFilter_Valid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var platform1 = new Platform() { Id = "1", CreatorId = "1", Name = "PC", Manufacturer = "PC" };

            var game1 = new Game() { Id = "1", CreatorId = "1", Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari" };
            var game2 = new Game() { Id = "2", CreatorId = "1", Title = "Space Invaders 2", Genre = "Arcade", Developer = "Atari", Publisher = "Atari" };

            var userGame1 = new UserGame { Id = "1", CreatorId = "1", User = new User(), Status = Status.Owned, Platform = platform1, Game = game1};
            var userGame2 = new UserGame { Id = "2", CreatorId = "1", User = new User(), Status = Status.Owned, Platform = platform1, Game = game2};

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(userGame1);
                context.Add(userGame2);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                UserGameRepository repo = new UserGameRepository(context);
                var uList = repo.GetByUserIdAndFilter(userGame1.CreatorId, null, null, null);

                //  Assert
                Assert.NotNull(uList);
                Assert.Single(uList.ToList());
            }
        }

        [Fact]
        public void GetByUserIdAndFilter_Invalid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var platform1 = new Platform() { Id = "1", CreatorId = "1", Name = "PC", Manufacturer = "PC" };

            var game1 = new Game() { Id = "1", CreatorId = "1", Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari" };
            var game2 = new Game() { Id = "2", CreatorId = "1", Title = "Space Invaders 2", Genre = "Arcade", Developer = "Atari", Publisher = "Atari" };

            var userGame1 = new UserGame { Id = "1", CreatorId = "1", User = new User(), Status = Status.Owned, Platform = platform1, Game = game1};
            var userGame2 = new UserGame { Id = "2", CreatorId = "1", User = new User(), Status = Status.Owned, Platform = platform1, Game = game2};
            var userGame3 = new UserGame { Id = "2", CreatorId = "2", User = new User(), Status = Status.Owned, Platform = platform1, Game = game2};

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(userGame1);
                context.Add(userGame2);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                UserGameRepository repo = new UserGameRepository(context);
                var uList = repo.GetByUserIdAndFilter(userGame3.CreatorId, null, null, null);

                //  Assert
                Assert.NotNull(uList);
                Assert.Empty(uList.ToList());
            }
        }
    }
}