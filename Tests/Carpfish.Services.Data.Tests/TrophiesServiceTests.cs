namespace Carpfish.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpfish.Data;
    using Carpfish.Data.Common.Repositories;
    using Carpfish.Data.Models;
    using Carpfish.Data.Repositories;
    using Carpfish.Services.Data.Tests.TestModels;
    using Carpfish.Services.Mapping;
    using Carpfish.Web.ViewModels.Trophies;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class TrophiesServiceTests
    {
        [Fact]
        public async Task AddAsyncShouldWorkCorrect()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);
            var trophyRepository = new EfDeletableEntityRepository<Trophy>(db);

            var clodinaryMock = new Mock<ICloudinaryService>();

            var service = new TrophiesService(clodinaryMock.Object, trophyRepository);

            // Arrange
            var fileMock = new Mock<IFormFile>();

            // Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.jpg";
            var ms = new MemoryStream();
            using var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var file = fileMock.Object;

            var input = new AddTrophyInputModel
            {
                LakeId = 1,
                BaitDescription = "corn",
                RigId = null,
                Weight = 12.500,
                MainImage = file,
                OtherImages = new List<IFormFile> { file },
            };

            Assert.Equal(0, service.GetCount());

            await service.AddAsync(input, Guid.NewGuid().ToString());
            await trophyRepository.SaveChangesAsync();

            Assert.Equal(1, service.GetCount());
        }

        [Fact]
        public async Task DeleteAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);

            var trophyRepository = new EfDeletableEntityRepository<Trophy>(db);

            var service = new TrophiesService(null, trophyRepository);

            await trophyRepository.AddAsync(new Trophy { Id = 1 });
            await trophyRepository.AddAsync(new Trophy { Id = 2 });
            await trophyRepository.AddAsync(new Trophy { Id = 3 });

            await trophyRepository.SaveChangesAsync();

            Assert.Equal(3, service.GetCount());

            await service.DeleteAsync(1);
            await trophyRepository.SaveChangesAsync();

            Assert.Equal(2, service.GetCount());

            await service.DeleteAsync(2);
            await trophyRepository.SaveChangesAsync();

            Assert.Equal(1, service.GetCount());
        }

        [Fact]
        public void GetAllShouldWorkCorrect()
        {
            var trophiesList = new List<Trophy>
            {
                new Trophy(),
                new Trophy(),
                new Trophy(),
            };

            var repository = new Mock<IDeletableEntityRepository<Trophy>>();
            repository.Setup(r => r.All()).Returns(trophiesList.AsQueryable());

            var service = new TrophiesService(null, repository.Object);
            AutoMapperConfig.RegisterMappings(typeof(TrophyModel).Assembly);

            Assert.Equal(3, service.GetAll<TrophyModel>(1, 6).Count());
        }

        [Fact]
        public void GetByIdShouldWorkCorrect()
        {
            var trophiesList = new List<Trophy>
            {
                new Trophy { Id = 1, BaitDescription = "a" },
                new Trophy { Id = 22, BaitDescription = "bc" },
            };

            var repository = new Mock<IDeletableEntityRepository<Trophy>>();
            repository.Setup(r => r.All()).Returns(trophiesList.AsQueryable());

            var service = new TrophiesService(null, repository.Object);
            AutoMapperConfig.RegisterMappings(typeof(TrophyModel).Assembly);

            var trophy = service.GetById<TrophyModel>(22);

            Assert.Equal(22, trophy.Id);
            Assert.Equal("bc", trophy.BaitDescription);
        }

        [Fact]
        public void GetCountShouldWorkCorrect()
        {
            var trophiesList = new List<Trophy>
            {
                new Trophy(),
                new Trophy(),
                new Trophy(),
            };

            var repository = new Mock<IDeletableEntityRepository<Trophy>>();
            repository.Setup(r => r.All()).Returns(trophiesList.AsQueryable());

            var service = new TrophiesService(null, repository.Object);

            Assert.Equal(3, service.GetCount());

            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public void GetTopFiveBiggestTrophiesShuldWorkCorrect()
        {
            var trophiesList = new List<Trophy>
            {
                new Trophy { Id = 1, Weight = 5 },
                new Trophy { Id = 2, Weight = 40 },
                new Trophy { Id = 3, Weight = 30 },
                new Trophy { Id = 4, Weight = 20 },
                new Trophy { Id = 5, Weight = 10 },
                new Trophy { Id = 6, Weight = 50 },
            };

            var repository = new Mock<IDeletableEntityRepository<Trophy>>();
            repository.Setup(r => r.All()).Returns(trophiesList.AsQueryable());

            var service = new TrophiesService(null, repository.Object);
            AutoMapperConfig.RegisterMappings(typeof(TrophyModel).Assembly);

            var trophiesResult = service.GetTopFiveBiggestTrophies<TrophyModel>();

            Assert.Equal(5, trophiesResult.Count());

            // biggest first
            Assert.Equal(6, trophiesResult.First().Id);
            Assert.Equal(50, trophiesResult.First().Weight);

            // smallest from top 5 last
            Assert.Equal(5, trophiesResult.Last().Id);
            Assert.Equal(10, trophiesResult.Last().Weight);
        }

        [Fact]
        public void GetLakeOwnerIdShouldWorkCorrectly()
        {
            var trophiesList = new List<Trophy>()
            {
                new Trophy { Id = 1, OwnerId = "asdas123" },
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);

            var repository = new Mock<IDeletableEntityRepository<Trophy>>();
            repository.Setup(r => r.All()).Returns(trophiesList.AsQueryable());

            var service = new TrophiesService(null, repository.Object);

            var result = service.GetTrophyOwnerId(1);

            Assert.Equal("asdas123", result);
        }

        [Fact]
        public async Task UpdateAsyncShouldWorkCorrect()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);
            var trophyRepository = new EfDeletableEntityRepository<Trophy>(db);

            var service = new TrophiesService(null, trophyRepository);

            await trophyRepository.AddAsync(new Trophy
            {
                Id = 1,
                Weight = 50,
                BaitDescription = "corn",
                LakeId = 1,
                OwnerId = "asdasd123",
            });

            await trophyRepository.SaveChangesAsync();

            var input = new EditTrophyInputModel
            {
                Weight = 49,
                BaitDescription = "grass",
            };

            await service.UpdateAsync(1, input);
            await trophyRepository.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(TrophyModel).Assembly);
            var trophy = service.GetById<TrophyModel>(1);

            Assert.Equal(49, trophy.Weight);
            Assert.Equal("grass", trophy.BaitDescription);
        }
    }
}
