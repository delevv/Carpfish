namespace Carpfish.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Carpfish.Data;
    using Carpfish.Data.Models;
    using Carpfish.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class VotesServiceTests
    {
        [Fact]
        public async Task GetLakeAverageVoteShouldWorkCorrect()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);
            var lakeRepository = new EfDeletableEntityRepository<Lake>(db);
            var lakeVoteRepository = new EfRepository<LakeVote>(db);
            var trophyVoteRepository = new EfRepository<TrophyVote>(db);
            var votesRepository = new EfRepository<Vote>(db);

            var service = new VotesService(lakeVoteRepository, trophyVoteRepository, votesRepository);

            await lakeRepository.AddAsync(new Lake { Id = 1 });
            await lakeRepository.AddAsync(new Lake { Id = 2 });

            await service.SetLakeVoteAsync(1, "someuserid123", 5);

            await service.SetLakeVoteAsync(1, "someseconduserid123", 4);

            await service.SetLakeVoteAsync(2, "someseconduserid123", 3);

            Assert.Equal(4.5, service.GetLakeAverageVote(1));
            Assert.Equal(3, service.GetLakeAverageVote(2));
        }

        [Fact]
        public void GetLakeAverageVoteShouldReturnIfLakeVotesIsNull()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);
            var lakeRepository = new EfDeletableEntityRepository<Lake>(db);
            var lakeVoteRepository = new EfRepository<LakeVote>(db);
            var trophyVoteRepository = new EfRepository<TrophyVote>(db);
            var votesRepository = new EfRepository<Vote>(db);

            var service = new VotesService(lakeVoteRepository, trophyVoteRepository, votesRepository);

            Assert.Equal(0, service.GetLakeAverageVote(1));
        }

        [Fact]
        public async Task GetLakeRatersCountShouldWorkCorrect()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);
            var lakeRepository = new EfDeletableEntityRepository<Lake>(db);
            var lakeVoteRepository = new EfRepository<LakeVote>(db);
            var trophyVoteRepository = new EfRepository<TrophyVote>(db);
            var votesRepository = new EfRepository<Vote>(db);

            var service = new VotesService(lakeVoteRepository, trophyVoteRepository, votesRepository);

            await lakeRepository.AddAsync(new Lake { Id = 1 });
            await lakeRepository.AddAsync(new Lake { Id = 2 });

            await service.SetLakeVoteAsync(1, "someuserid123", 5);

            Assert.Equal(1, service.GetLakeRatersCount(1));
            Assert.Equal(0, service.GetLakeRatersCount(2));
        }

        [Fact]
        public async Task GetLakeRatersCountByValueShouldWorkCorrect()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);
            var lakeRepository = new EfDeletableEntityRepository<Lake>(db);
            var lakeVoteRepository = new EfRepository<LakeVote>(db);
            var trophyVoteRepository = new EfRepository<TrophyVote>(db);
            var votesRepository = new EfRepository<Vote>(db);

            var service = new VotesService(lakeVoteRepository, trophyVoteRepository, votesRepository);

            await lakeRepository.AddAsync(new Lake { Id = 1 });
            await lakeRepository.AddAsync(new Lake { Id = 2 });

            await service.SetLakeVoteAsync(1, "someuserid123", 5);
            await service.SetLakeVoteAsync(1, "someuserid1234", 5);
            await service.SetLakeVoteAsync(2, "someuserid12345", 2);

            Assert.Equal(2, service.GetLakeRatersCountByValue(1, 5));
            Assert.Equal(0, service.GetLakeRatersCountByValue(1, 4));

            Assert.Equal(1, service.GetLakeRatersCountByValue(2, 2));
        }

        [Fact]
        public async Task GetLakeRatersCountByValueShouldReturnZeroWithoutVotes()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);
            var lakeRepository = new EfDeletableEntityRepository<Lake>(db);
            var lakeVoteRepository = new EfRepository<LakeVote>(db);
            var trophyVoteRepository = new EfRepository<TrophyVote>(db);
            var votesRepository = new EfRepository<Vote>(db);

            var service = new VotesService(lakeVoteRepository, trophyVoteRepository, votesRepository);

            await lakeRepository.AddAsync(new Lake { Id = 1 });
            await lakeRepository.AddAsync(new Lake { Id = 2 });

            Assert.Equal(0, service.GetLakeRatersCountByValue(1, 5));
            Assert.Equal(0, service.GetLakeRatersCountByValue(2, 4));
        }

        [Fact]
        public async Task GetTrophyAverageVoteShouldWorkCorrect()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);
            var trophyRepository = new EfDeletableEntityRepository<Trophy>(db);
            var lakeVoteRepository = new EfRepository<LakeVote>(db);
            var trophyVoteRepository = new EfRepository<TrophyVote>(db);
            var votesRepository = new EfRepository<Vote>(db);

            var service = new VotesService(lakeVoteRepository, trophyVoteRepository, votesRepository);

            await trophyRepository.AddAsync(new Trophy { Id = 1 });
            await trophyRepository.AddAsync(new Trophy { Id = 2 });

            await service.SetTrophyVoteAsync(1, "someuserid123", 5);

            await service.SetTrophyVoteAsync(1, "someseconduserid123", 4);

            await service.SetTrophyVoteAsync(2, "someseconduserid123", 3);

            Assert.Equal(4.5, service.GetTrophyAverageVote(1));
            Assert.Equal(3, service.GetTrophyAverageVote(2));
        }

        [Fact]
        public async Task GetTrophyAverageVoteShouldReturnZeroWithoutVotes()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);
            var trophyRepository = new EfDeletableEntityRepository<Trophy>(db);
            var lakeVoteRepository = new EfRepository<LakeVote>(db);
            var trophyVoteRepository = new EfRepository<TrophyVote>(db);
            var votesRepository = new EfRepository<Vote>(db);

            var service = new VotesService(lakeVoteRepository, trophyVoteRepository, votesRepository);

            await trophyRepository.AddAsync(new Trophy { Id = 1 });
            await trophyRepository.AddAsync(new Trophy { Id = 2 });

            Assert.Equal(0, service.GetTrophyAverageVote(1));
            Assert.Equal(0, service.GetTrophyAverageVote(2));
        }

        [Fact]
        public async Task GetTrophyRatersCountShouldWorkCorrect()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);
            var trophyRepository = new EfDeletableEntityRepository<Trophy>(db);
            var lakeVoteRepository = new EfRepository<LakeVote>(db);
            var trophyVoteRepository = new EfRepository<TrophyVote>(db);
            var votesRepository = new EfRepository<Vote>(db);

            var service = new VotesService(lakeVoteRepository, trophyVoteRepository, votesRepository);

            await trophyRepository.AddAsync(new Trophy { Id = 1 });
            await trophyRepository.AddAsync(new Trophy { Id = 2 });
            await trophyRepository.AddAsync(new Trophy { Id = 3 });

            await service.SetTrophyVoteAsync(1, "someuserid123", 5);
            await service.SetTrophyVoteAsync(3, "someuserid1234", 5);
            await service.SetTrophyVoteAsync(3, "someuserid1235", 5);

            Assert.Equal(1, service.GetTrophyRatersCount(1));
            Assert.Equal(0, service.GetTrophyRatersCount(2));
            Assert.Equal(2, service.GetTrophyRatersCount(3));
        }

        [Fact]
        public async Task SetLakeVoteShouldUpdateVoteIfVoteAlreadyExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);
            var lakeRepository = new EfDeletableEntityRepository<Lake>(db);
            var lakeVoteRepository = new EfRepository<LakeVote>(db);
            var trophyVoteRepository = new EfRepository<TrophyVote>(db);
            var votesRepository = new EfRepository<Vote>(db);

            var service = new VotesService(lakeVoteRepository, trophyVoteRepository, votesRepository);

            await lakeRepository.AddAsync(new Lake { Id = 1 });
            await lakeRepository.AddAsync(new Lake { Id = 2 });

            await service.SetLakeVoteAsync(1, "user123", 5);
            await service.SetLakeVoteAsync(2, "user1234", 3);

            Assert.Equal(5, service.GetLakeAverageVote(1));
            Assert.Equal(3, service.GetLakeAverageVote(2));

            await service.SetLakeVoteAsync(1, "user123", 4);
            await service.SetLakeVoteAsync(2, "user1234", 2);

            Assert.Equal(4, service.GetLakeAverageVote(1));
            Assert.Equal(2, service.GetLakeAverageVote(2));
        }

        [Fact]
        public async Task SetTrophyVoteShouldUpdateVoteIfVoteAlreadyExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var db = new ApplicationDbContext(options);
            var trophyRepository = new EfDeletableEntityRepository<Trophy>(db);
            var lakeVoteRepository = new EfRepository<LakeVote>(db);
            var trophyVoteRepository = new EfRepository<TrophyVote>(db);
            var votesRepository = new EfRepository<Vote>(db);

            var service = new VotesService(lakeVoteRepository, trophyVoteRepository, votesRepository);

            await trophyRepository.AddAsync(new Trophy { Id = 1 });
            await trophyRepository.AddAsync(new Trophy { Id = 2 });

            await service.SetTrophyVoteAsync(1, "user123", 5);
            await service.SetTrophyVoteAsync(2, "user1234", 3);

            Assert.Equal(5, service.GetTrophyAverageVote(1));
            Assert.Equal(3, service.GetTrophyAverageVote(2));

            await service.SetTrophyVoteAsync(1, "user123", 4);
            await service.SetTrophyVoteAsync(2, "user1234", 2);

            Assert.Equal(4, service.GetTrophyAverageVote(1));
            Assert.Equal(2, service.GetTrophyAverageVote(2));
        }
    }
}
