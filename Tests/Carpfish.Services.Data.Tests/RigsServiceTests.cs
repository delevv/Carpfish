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
    using Carpfish.Web.ViewModels.Materials;
    using Carpfish.Web.ViewModels.Rigs;
    using Carpfish.Web.ViewModels.Steps;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class RigsServiceTests
    {
        [Fact]
        public async Task AddAsyncShouldWorkCorrect()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);
            var rigsRepository = new EfDeletableEntityRepository<Rig>(db);

            var clodinaryMock = new Mock<ICloudinaryService>();

            var service = new RigsService(clodinaryMock.Object, rigsRepository);

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

            var input = new AddRigInputModel
            {
                Name = "Test Rig",
                Description = "This is a test rig",
                Image = file,
                Steps = new List<AddStepInputModel>
                {
                    new AddStepInputModel
                    {
                        Description = "test step 1",
                        Image = file,
                    },
                    new AddStepInputModel
                    {
                        Description = "test step 2",
                        Image = file,
                    },
                },
                Materials = new List<AddMaterialInputModel>
                {
                    new AddMaterialInputModel { Description = "test material 1" },
                    new AddMaterialInputModel { Description = "test material 2" },
                },
            };

            Assert.Equal(0, service.GetCount());

            await service.AddAsync(input, Guid.NewGuid().ToString());
            await rigsRepository.SaveChangesAsync();

            Assert.Equal(1, service.GetCount());
        }

        [Fact]
        public async Task DeleteAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);

            var rigsRepository = new EfDeletableEntityRepository<Rig>(db);

            var service = new RigsService(null, rigsRepository);

            await rigsRepository.AddAsync(new Rig { Id = 1 });
            await rigsRepository.AddAsync(new Rig { Id = 2 });
            await rigsRepository.AddAsync(new Rig { Id = 3 });

            await rigsRepository.SaveChangesAsync();

            Assert.Equal(3, service.GetCount());

            await service.DeleteAsync(1);
            await rigsRepository.SaveChangesAsync();

            Assert.Equal(2, service.GetCount());

            await service.DeleteAsync(2);
            await rigsRepository.SaveChangesAsync();

            Assert.Equal(1, service.GetCount());
        }

        [Fact]
        public void GetAllShouldWorkCorrect()
        {
            var rigsList = new List<Rig>
            {
                new Rig(),
                new Rig(),
                new Rig(),
            };

            var repository = new Mock<IDeletableEntityRepository<Rig>>();
            repository.Setup(r => r.All()).Returns(rigsList.AsQueryable());

            var service = new RigsService(null, repository.Object);
            AutoMapperConfig.RegisterMappings(typeof(RigModel).Assembly);

            Assert.Equal(3, service.GetAll<RigModel>(1, 6).Count());
        }

        [Fact]
        public void GetAllAsKeyValuePairsShouldWorkCorrect()
        {
            var rigsList = new List<Rig>
            {
                new Rig { Id = 1, Name = "test1" },
                new Rig { Id = 2, Name = "test2" },
                new Rig { Id = 3, Name = "test3" },
            };

            var repository = new Mock<IDeletableEntityRepository<Rig>>();
            repository.Setup(r => r.All()).Returns(rigsList.AsQueryable());

            var service = new RigsService(null, repository.Object);

            var result = service.GetAllAsKeyValuePairs()
                .Select(x => x.Key + x.Value)
                .ToList();

            Assert.Equal(3, result.Count());
            Assert.Equal("1test1", result[0]);
            Assert.Equal("2test2", result[1]);
            Assert.Equal("3test3", result[2]);
        }

        [Fact]
        public void GetByIdShouldWorkCorrect()
        {
            var rigsList = new List<Rig>
            {
                new Rig { Id = 1, Name = "a" },
                new Rig { Id = 22, Name = "test" },
            };

            var repository = new Mock<IDeletableEntityRepository<Rig>>();
            repository.Setup(r => r.All()).Returns(rigsList.AsQueryable());

            var service = new RigsService(null, repository.Object);
            AutoMapperConfig.RegisterMappings(typeof(RigModel).Assembly);

            var rig = service.GetById<RigModel>(22);

            Assert.Equal(22, rig.Id);
            Assert.Equal("test", rig.Name);
        }

        [Fact]
        public void GetCountShouldWorkCorrect()
        {
            var rigsList = new List<Rig>
            {
                new Rig(),
                new Rig(),
                new Rig(),
            };

            var repository = new Mock<IDeletableEntityRepository<Rig>>();
            repository.Setup(r => r.All()).Returns(rigsList.AsQueryable());

            var service = new RigsService(null, repository.Object);

            Assert.Equal(3, service.GetCount());
        }

        [Fact]
        public void GetFiveRigsWithMostTrophiesShouldWorkCorrect()
        {
            var rigsList = new List<Rig>
            {
                new Rig { Id = 1, Trophies = new List<Trophy> { new Trophy() } },
                new Rig { Id = 2, Trophies = new List<Trophy> { new Trophy(), new Trophy(), new Trophy(), } },
                new Rig { Id = 3, Trophies = new List<Trophy> { new Trophy(), new Trophy(), new Trophy(), new Trophy(), } },
                new Rig { Id = 4, Trophies = new List<Trophy> { new Trophy(), new Trophy(), new Trophy(), new Trophy(), new Trophy(), } },
                new Rig { Id = 5, Trophies = new List<Trophy> { new Trophy(), new Trophy() } },
                new Rig { Id = 6 },
            };

            var repository = new Mock<IDeletableEntityRepository<Rig>>();
            repository.Setup(r => r.All()).Returns(rigsList.AsQueryable());

            var service = new RigsService(null, repository.Object);
            AutoMapperConfig.RegisterMappings(typeof(RigModel).Assembly);

            var rigs = service.GetFiveRigsWithMostTrophies<RigModel>();

            Assert.Equal(5, rigs.Count());

            // most trophies rig first
            Assert.Equal(5, rigs.First().Trophies.Count());
            Assert.Equal(4, rigs.First().Id);

            // less trophies rig last
            Assert.Single(rigs.Last().Trophies);
            Assert.Equal(1, rigs.Last().Id);
        }

        [Fact]
        public void GetRigOwnerIdShouldWorkCorrectly()
        {
            var rigsList = new List<Rig>()
            {
                new Rig { Id = 1, OwnerId = "asdas123" },
            };

            var repository = new Mock<IDeletableEntityRepository<Rig>>();
            repository.Setup(r => r.All()).Returns(rigsList.AsQueryable());

            var service = new RigsService(null, repository.Object);

            var result = service.GetRigOwnerId(1);

            Assert.Equal("asdas123", result);
        }

        [Fact]
        public async Task UpdateAsyncShouldWorkCorrect()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);
            var rigRepository = new EfDeletableEntityRepository<Rig>(db);

            var service = new RigsService(null, rigRepository);

            await rigRepository.AddAsync(new Rig
            {
                Id = 22,
                Steps = new List<Step> { new Step() },
                Description = "initial test description",
                Name = "initial name",
                OwnerId = "t1t2t3",
            });

            await rigRepository.SaveChangesAsync();

            var input = new EditRigInputModel
            {
                Name = "updated name",
                Description = "updated description",
            };

            await service.UpdateAsync(22, input);
            await rigRepository.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(RigModel).Assembly);
            var rig = service.GetById<RigModel>(22);

            Assert.Equal("updated name", rig.Name);
            Assert.Equal("updated description", rig.Description);
        }
    }
}
