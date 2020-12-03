namespace Carpfish.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpfish.Data.Common.Repositories;
    using Carpfish.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IRepository<LakeVote> lakeVotesRepository;
        private readonly IRepository<TrophyVote> trophyVotesRepository;

        public VotesService(IRepository<LakeVote> lakeVotesRepository, IRepository<TrophyVote> trophyVotesRepository)
        {
            this.lakeVotesRepository = lakeVotesRepository;
            this.trophyVotesRepository = trophyVotesRepository;
        }

        public double GetLakeAverageVote(int lakeId)
        {
            return this.lakeVotesRepository.All()
                .Where(lv => lv.LakeId == lakeId)
                .Average(lv => lv.Vote.Value);
        }

        public int GetLakeRatersCount(int lakeId)
        {
            return this.lakeVotesRepository.All()
                .Where(lv => lv.LakeId == lakeId)
                .Count();
        }

        public int GetLakeRatersCountByValue(int lakeId, int value)
        {
            return this.lakeVotesRepository.All()
                .Where(lv => lv.LakeId == lakeId && lv.Vote.Value == value)
                .Count();
        }

        public double GetTrophyAverageVote(int trophyId)
        {
            return this.trophyVotesRepository.All()
                .Where(tv => tv.TrophyId == trophyId)
                .Average(tv => tv.Vote.Value);
        }

        public int GetTrophyRatersCount(int trophyId)
        {
            return this.trophyVotesRepository.All()
               .Where(tv => tv.TrophyId == trophyId)
               .Count();
        }

        public async Task SetLakeVoteAsync(int lakeId, string userId, byte value)
        {
            var lakeVote = this.lakeVotesRepository.All()
                .FirstOrDefault(lv => lv.LakeId == lakeId && lv.UserId == userId);

            if (lakeVote == null)
            {
                lakeVote = new LakeVote
                {
                    LakeId = lakeId,
                    UserId = userId,
                    Vote = new Vote
                    {
                        Value = value,
                    },
                };

                await this.lakeVotesRepository.AddAsync(lakeVote);
            }
            else
            {
                lakeVote.Vote.Value = value;
            }

            await this.lakeVotesRepository.SaveChangesAsync();
        }

        public async Task SetTrophyVoteAsync(int trophyId, string userId, byte value)
        {
            var trophyVote = this.trophyVotesRepository.All()
                .FirstOrDefault(tv => tv.TrophyId == trophyId && tv.UserId == userId);

            if (trophyVote == null)
            {
                trophyVote = new TrophyVote
                {
                    TrophyId = trophyId,
                    UserId = userId,
                    Vote = new Vote
                    {
                        Value = value,
                    },
                };

                await this.trophyVotesRepository.AddAsync(trophyVote);
            }
            else
            {
                trophyVote.Vote.Value = value;
            }

            await this.trophyVotesRepository.SaveChangesAsync();
        }
    }
}
