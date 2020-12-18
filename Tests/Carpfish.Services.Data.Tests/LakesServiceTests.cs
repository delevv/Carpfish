namespace Carpfish.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Carpfish.Data;
    using Carpfish.Data.Common.Repositories;
    using Carpfish.Data.Models;
    using Carpfish.Data.Repositories;
    using Carpfish.Services.Mapping;
    using Carpfish.Web.ViewModels.Lakes;
    using Carpfish.Web.ViewModels.Location;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Primitives;
    using Moq;
    using Xunit;

    public class LakesServiceTests
    {
        [Fact]
        public void GetCountShouldWorkCorrect()
        {
            var repository = new Mock<IDeletableEntityRepository<Lake>>();
            repository.Setup(r => r.All()).Returns(new List<Lake>
                                                        {
                                                            new Lake(),
                                                            new Lake(),
                                                            new Lake(),
                                                            new Lake(),
                                                        }.AsQueryable());

            var service = new LakesService(null, repository.Object);

            Assert.Equal(4, service.GetCount());
        }

        [Fact]
        public void GetByIdShouldWorkCorrect()
        {
            var repository = new Mock<IDeletableEntityRepository<Lake>>();
            repository.Setup(r => r.All()).Returns(new List<Lake>
                                                        {
                                                            new Lake
                                                                {
                                                                Id = 1,
                                                                Name = "Test",
                                                                },
                                                            new Lake
                                                               {
                                                                Id = 22,
                                                                Name = "Test22",
                                                               },
                                                            new Lake(),
                                                        }.AsQueryable());

            var service = new LakesService(null, repository.Object);
            AutoMapperConfig.RegisterMappings(typeof(LakeModel).Assembly);

            Assert.Equal(1, service.GetById<LakeModel>(1).Id);
            Assert.Equal("Test", service.GetById<LakeModel>(1).Name);

            Assert.Equal(22, service.GetById<LakeModel>(22).Id);
            Assert.Equal("Test22", service.GetById<LakeModel>(22).Name);
        }

        [Fact]
        public void GetAllWithoutParametersShouldWorkCorrect()
        {
            var repository = new Mock<IDeletableEntityRepository<Lake>>();
            repository.Setup(r => r.All()).Returns(new List<Lake>
                                                        {
                                                            new Lake(),
                                                            new Lake(),
                                                            new Lake(),
                                                        }.AsQueryable());

            var service = new LakesService(null, repository.Object);
            AutoMapperConfig.RegisterMappings(typeof(LakeModel).Assembly);

            Assert.Equal(3, service.GetAll<LakeModel>().Count());
        }

        [Fact]
        public void GetAllWithTwoParametersShouldWorkCorrect()
        {
            var repository = new Mock<IDeletableEntityRepository<Lake>>();
            repository.Setup(r => r.All()).Returns(new List<Lake>
                                                        {
                                                            new Lake(),
                                                            new Lake(),
                                                            new Lake(),
                                                        }.AsQueryable());

            var service = new LakesService(null, repository.Object);
            AutoMapperConfig.RegisterMappings(typeof(LakeModel).Assembly);

            Assert.Equal(3, service.GetAll<LakeModel>(1, 6).Count());
        }

        [Fact]
        public void GetFreeLakesCountShouldWorkCorrect()
        {
            var repository = new Mock<IDeletableEntityRepository<Lake>>();
            repository.Setup(r => r.All()).Returns(new List<Lake>
                                                        {
                                                            new Lake
                                                            {
                                                                IsFree = true,
                                                            },
                                                            new Lake(),
                                                            new Lake
                                                            {
                                                                IsFree = true,
                                                            },
                                                        }.AsQueryable());

            var service = new LakesService(null, repository.Object);

            Assert.Equal(2, service.GetFreeLakesCount());
        }

        [Fact]
        public void GetPaidLakesCountShouldWorkCorrect()
        {
            var repository = new Mock<IDeletableEntityRepository<Lake>>();
            repository.Setup(r => r.All()).Returns(new List<Lake>
                                                        {
                                                            new Lake
                                                            {
                                                                IsFree = true,
                                                            },
                                                            new Lake(),
                                                            new Lake(),
                                                        }.AsQueryable());

            var service = new LakesService(null, repository.Object);

            Assert.Equal(2, service.GetPaidLakesCount());
        }

        [Fact]
        public void GetAllWithThreeParametersFirstOfThemFreeShouldWorkCorrect()
        {
            var repository = new Mock<IDeletableEntityRepository<Lake>>();
            repository.Setup(r => r.All()).Returns(new List<Lake>
                                                        {
                                                            new Lake(),
                                                            new Lake(),
                                                            new Lake(),
                                                        }.AsQueryable());

            var service = new LakesService(null, repository.Object);
            AutoMapperConfig.RegisterMappings(typeof(LakeModel).Assembly);

            Assert.Empty(service.GetAll<LakeModel>("Free", 1, 6));
        }

        [Fact]
        public void GetAllWithThreeParametersFirstOfThemPaidShouldWorkCorrect()
        {
            var repository = new Mock<IDeletableEntityRepository<Lake>>();
            repository.Setup(r => r.All()).Returns(new List<Lake>
                                                        {
                                                            new Lake(),
                                                            new Lake(),
                                                            new Lake(),
                                                        }.AsQueryable());

            var service = new LakesService(null, repository.Object);
            AutoMapperConfig.RegisterMappings(typeof(LakeModel).Assembly);

            Assert.Equal(3, service.GetAll<LakeModel>("Paid", 1, 6).Count());
        }

        [Fact]
        public void GetAllAsKeyValuePairsShouldWorkCorrect()
        {
            var repository = new Mock<IDeletableEntityRepository<Lake>>();
            repository.Setup(r => r.All()).Returns(new List<Lake>
                                                        {
                                                            new Lake
                                                            {
                                                                Id = 1,
                                                                Name = "test1",
                                                            },
                                                            new Lake
                                                            {
                                                                Id = 2,
                                                                Name = "test2",
                                                            },
                                                            new Lake
                                                            {
                                                                Id = 3,
                                                                Name = "test3",
                                                            },
                                                        }.AsQueryable());

            var service = new LakesService(null, repository.Object);

            var result = service.GetAllAsKeyValuePairs()
                .Select(x => x.Key + x.Value)
                .ToList();

            Assert.Equal(3, result.Count());
            Assert.Equal("1test1", result[0]);
            Assert.Equal("2test2", result[1]);
            Assert.Equal("3test3", result[2]);
        }

        [Fact]
        public void GetLakeOwnerIdShouldWorkCorrectly()
        {
            var repository = new Mock<IDeletableEntityRepository<Lake>>();
            repository.Setup(r => r.All()).Returns(new List<Lake>
                                                        {
                                                            new Lake
                                                            {
                                                                Id = 1,
                                                                Name = "test1",
                                                                OwnerId = "asdas123",
                                                            },
                                                        }.AsQueryable());

            var service = new LakesService(null, repository.Object);

            var result = service.GetLakeOwnerId(1);

            Assert.Equal("asdas123", result);
        }

        [Fact]
        public async Task DeleteAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);
            var lakeRepository = new EfDeletableEntityRepository<Lake>(db);

            var service = new LakesService(null, lakeRepository);

            await lakeRepository.AddAsync(new Lake
            {
                Id = 1,
            });

            await lakeRepository.AddAsync(new Lake
            {
                Id = 2,
            });

            await lakeRepository.AddAsync(new Lake
            {
                Id = 3,
            });

            await lakeRepository.SaveChangesAsync();

            Assert.Equal(3, service.GetCount());

            await service.DeleteAsync(1);
            await lakeRepository.SaveChangesAsync();

            Assert.Equal(2, service.GetCount());

            await service.DeleteAsync(2);
            await lakeRepository.SaveChangesAsync();

            Assert.Equal(1, service.GetCount());
        }

        [Fact]
        public async Task UpdateAsyncShouldWorkCorrect()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);
            var lakeRepository = new EfDeletableEntityRepository<Lake>(db);

            var service = new LakesService(null, lakeRepository);

            await lakeRepository.AddAsync(new Lake
            {
                Id = 1,
                Name = "Test",
                WebsiteUrl = "www.test.com",
                Area = 11.11,
                IsFree = false,
            });

            await lakeRepository.SaveChangesAsync();

            var input = new EditLakeInputModel
            {
                CountryId = 2,
                Name = "Test2",
                WebsiteUrl = "www.test2.com",
                Area = 22.22,
                IsFree = true,
            };

            await service.UpdateAsync(1, input);
            await lakeRepository.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(LakeModel).Assembly);
            var currLake = service.GetById<LakeModel>(1);

            Assert.Equal(2, currLake.CountryId);
        }

        [Fact]
        public async Task AddAsyncShouldWorkCorrect()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);
            var lakeRepository = new EfDeletableEntityRepository<Lake>(db);

            var clodinaryMock = new Mock<ICloudinaryService>();

            var service = new LakesService(clodinaryMock.Object, lakeRepository);

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

            var input = new AddLakeInputModel
            {
                Location = new LocationLakeInputModel
                {
                    Latitude = 12.22,
                    Longitude = 13.33,
                },
                CountryId = 2,
                Name = "Test2",
                WebsiteUrl = "www.test2.com",
                Area = 22.22,
                IsFree = true,
                MainImage = file,
                OtherImages = new List<IFormFile> { file },
            };

            Assert.Equal(0, service.GetCount());

            await service.AddAsync(input, Guid.NewGuid().ToString());
            await lakeRepository.SaveChangesAsync();

            Assert.Equal(1, service.GetCount());
        }
    }
}
