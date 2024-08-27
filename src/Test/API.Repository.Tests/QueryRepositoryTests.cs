using Microsoft.EntityFrameworkCore;
using MyGameStat.Domain.Entity;
using MyGameStat.Infrastructure.Persistence;
using MyGameStat.Infrastructure.Repository;

namespace Test.API.Repository.Tests {
    public class QueryRepositoryTests {
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

            Platform platform1 = new Platform { Id = "1", CreatorId = "1", Name = "PC", Manufacturer = "PC" };
            Platform platform2 = new Platform { Id = "2", CreatorId = "1", Name = "Xbox", Manufacturer = "Microsoft" };

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(platform1);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                PlatformRepository repo = new PlatformRepository(context);
                Platform? platform = repo.GetById("1");

                //  Assert
                Assert.NotNull(platform);
            }
        }

        [Fact]
        public void GetById_Invalid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Platform platform1 = new Platform { Id = "1", CreatorId = "1", Name = "PC", Manufacturer = "PC" };
            Platform platform2 = new Platform { Id = "2", CreatorId = "1", Name = "Xbox", Manufacturer = "Microsoft" };

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(platform1);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                PlatformRepository repo = new PlatformRepository(context);
                Platform? platform = repo.GetById("2");

                //  Assert
                Assert.Null(platform);
            }
        }

        [Fact]
        public void GetAll_Valid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Platform platform1 = new Platform { Id = "1", CreatorId = "1", Name = "PC", Manufacturer = "PC" };
            Platform platform2 = new Platform { Id = "2", CreatorId = "1", Name = "Xbox", Manufacturer = "Microsoft" };

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(platform1);
                context.Add(platform2);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                PlatformRepository repo = new PlatformRepository(context);
                var list = repo.GetAll();

                //  Assert
                Assert.NotNull(list);
                Assert.Equal(2, list.ToList().Count);
            }
        }
    }
}