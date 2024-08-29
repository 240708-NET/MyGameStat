using Microsoft.EntityFrameworkCore;
using MyGameStat.Domain.Entity;
using MyGameStat.Infrastructure.Persistence;
using MyGameStat.Infrastructure.Repository;

namespace Test.API.Repository.Tests {
    public class GameRepositoryTests {
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
                context.Add(new Game { Id = "1", CreatorId = "1", Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari" });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                GameRepository repo = new GameRepository(context);
                Game? game = repo.GetById("1");

                //  Assert
                Assert.NotNull(game);
            }
        }

        [Fact]
        public void GetById_Invalid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(new Game { Id = "1", CreatorId = "1", Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari" });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                GameRepository repo = new GameRepository(context);
                Game? game = repo.GetById("4");

                //  Assert
                Assert.Null(game);
            }
        }

        [Fact]
        public void GetByTitle_Valid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(new Game { Id = "1", CreatorId = "1", Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari" });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                GameRepository repo = new GameRepository(context);
                var list = repo.GetByTitle("Space Invaders");

                //  Assert
                Assert.NotNull(list);
                Assert.Single(list.ToList());
            }
        }

        [Fact]
        public void GetByTitle_Invalid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(new Game { Id = "1", CreatorId = "1", Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari" });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                GameRepository repo = new GameRepository(context);
                var list = repo.GetByTitle("Space Invaders 4");

                //  Assert
                Assert.NotNull(list);
                Assert.Empty(list.ToList());
            }
        }

        [Fact]
        public void Retrieve_Valid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(new Game { Id = "1", CreatorId = "1", Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari" });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                GameRepository repo = new GameRepository(context);
                var game = repo.Retrieve(new Game { CreatorId = "1", Title = "Space Invaders", Genre = "Arcade", Developer = "Atari", Publisher = "Atari" });

                //  Assert
                Assert.NotNull(game);
                Assert.Equal("Space Invaders", game.Title);
            }
        }
    }
}
