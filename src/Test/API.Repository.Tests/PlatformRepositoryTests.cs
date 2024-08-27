using Microsoft.EntityFrameworkCore;
using MyGameStat.Domain.Entity;
using MyGameStat.Infrastructure.Persistence;
using MyGameStat.Infrastructure.Repository;

namespace Test.API.Repository.Tests {
    public class PlatformRepositoryTests {
        private DbContextOptions<ApplicationDbContext>? _options;
        
        [Fact]
        public void SanityCheck() {
            Assert.True(1 == 1);
        }

        [Fact]
        public void Retrieve_Valid() {
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
                Platform? platform = repo.Retrieve(platform1);

                //  Assert
                Assert.NotNull(platform);
            }
        }

        [Fact]
        public void Retrieve_Invalid() {
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
                Platform? platform = repo.Retrieve(platform2);

                //  Assert
                Assert.Null(platform);
            }
        }
    }
}