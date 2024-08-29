using Microsoft.EntityFrameworkCore;
using MyGameStat.Domain.Entity;
using MyGameStat.Infrastructure.Persistence;
using MyGameStat.Infrastructure.Repository;

namespace Test.API.Repository.Tests {
    public class RepositoryTests {
        private DbContextOptions<ApplicationDbContext>? _options;
        
        [Fact]
        public void SanityCheck() {
            Assert.True(1 == 1);
        }

        [Fact]
        public void Save_Valid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Platform platform1 = new Platform { Id = "1", CreatorId = "1", Name = "PC", Manufacturer = "PC" };

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                PlatformRepository repo = new PlatformRepository(context);
                Platform? platform = repo.Save(platform1);

                //  Assert
                Assert.NotNull(platform);
                Assert.Equal("1", platform.Id);
            }
        }

        [Fact]
        public void Delete_Valid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Platform platform1 = new Platform { Id = "1", CreatorId = "1", Name = "PC", Manufacturer = "PC" };

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(platform1);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                PlatformRepository repo = new PlatformRepository(context);
                repo.Delete("1");

                var list = repo.GetAll();

                //  Assert
                Assert.NotNull(list);
                Assert.Empty(list.ToList());
            }
        }

        [Fact]
        public void Delete_Invalid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Platform platform1 = new Platform { Id = "1", CreatorId = "1", Name = "PC", Manufacturer = "PC" };

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(platform1);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                PlatformRepository repo = new PlatformRepository(context);
                repo.Delete("2");

                var list = repo.GetAll();

                //  Assert
                Assert.NotNull(list);
                Assert.Single(list.ToList());
            }
        }

        [Fact]
        public void Update_Valid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Platform platform1 = new Platform { Id = "1", CreatorId = "1", Name = "PC", Manufacturer = "PC" };

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(platform1);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                PlatformRepository repo = new PlatformRepository(context);
                platform1.Name = "Xbox";
                var result = repo.Update(platform1);

                //  Assert
                Assert.Equal(1, result);

                var platform = repo.GetById("1");
                Assert.NotNull(platform);
                Assert.Equal("Xbox", platform.Name);
            }
        }

        [Fact]
        public void Update_InvalidId() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Platform platform1 = new Platform { Id = "1", CreatorId = "1", Name = "PC", Manufacturer = "PC" };

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(platform1);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                PlatformRepository repo = new PlatformRepository(context);
                platform1.Id = null;
                platform1.Name = "Xbox";
                var result = repo.Update(platform1);

                //  Assert
                Assert.Equal(0, result);
            }
        }

        [Fact]
        public void Update_InvalidEntity() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Platform platform1 = new Platform { Id = "1", CreatorId = "1", Name = "PC", Manufacturer = "PC" };

            using (var context = new ApplicationDbContext(_options)) {
                //context.Add(platform1);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                PlatformRepository repo = new PlatformRepository(context);
                platform1.Name = "Xbox";
                var result = repo.Update(platform1);

                //  Assert
                Assert.Equal(0, result);
            }
        }

        [Fact]
        public void Retrieve_Valid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Platform platform1 = new Platform { Id = "1", CreatorId = "1", Name = "PC", Manufacturer = "PC" };

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(platform1);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                PlatformRepository repo = new PlatformRepository(context);
                Platform? platform = repo.Retrieve(platform1);

                //  Assert
                Assert.NotNull(platform);
            }
        }
    }
}
