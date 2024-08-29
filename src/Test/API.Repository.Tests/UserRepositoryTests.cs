using Microsoft.EntityFrameworkCore;
using MyGameStat.Domain.Entity;
using MyGameStat.Infrastructure.Persistence;
using MyGameStat.Infrastructure.Repository;

namespace Test.API.Repository.Tests {
    public class UserRepositoryTests {
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
                context.Add(new User { Id = "1" });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                UserRepository repo = new UserRepository(context);
                User? user = repo.GetById("1");

                //  Assert
                Assert.NotNull(user);
            }
        }

        [Fact]
        public void GetById_Invalid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(new User { Id = "1" });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                UserRepository repo = new UserRepository(context);
                User? user = repo.GetById("2");

                //  Assert
                Assert.Null(user);
            }
        }
    
        [Fact]
        public void GetByUserName_Valid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(new User { Id = "1", UserName = "Bob" });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                UserRepository repo = new UserRepository(context);
                User? user = repo.GetByUserName("Bob");

                //  Assert
                Assert.NotNull(user);
            }
        }

        [Fact]
        public void GetByUserName_Invalid() {
            //  Arrange
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDbContext(_options)) {
                context.Add(new User { Id = "1", UserName = "Bob" });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(_options)) {
                //  Act
                UserRepository repo = new UserRepository(context);
                User? user = repo.GetByUserName("Steve");

                //  Assert
                Assert.Null(user);
            }
        }
    }
}
