namespace Carpfish.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpfish.Data;
    using Carpfish.Data.Models;
    using Carpfish.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CountriesServiceTests
    {
        [Fact]
        public async Task GetAllAsKeyValuePairsShouldWorkCorrect()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);

            var repository = new EfRepository<Country>(db);

            await repository.AddAsync(new Country { Id = 1, Name = "Bulgaria" });
            await repository.AddAsync(new Country { Id = 2, Name = "Nigeria" });
            await repository.AddAsync(new Country { Id = 3, Name = "Germany" });

            await repository.SaveChangesAsync();

            var service = new CountriesService(repository);

            var result = service.GetAllAsKeyValuePairs()
                .Select(x => x.Key + "=>" + x.Value)
                .ToList();

            Assert.Equal(3, result.Count());

            // order should be by name
            Assert.Equal("1=>Bulgaria", result[0]);
            Assert.Equal("2=>Nigeria", result[2]);
            Assert.Equal("3=>Germany", result[1]);
        }
    }
}
