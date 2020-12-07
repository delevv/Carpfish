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
        private readonly IRepository<Vote> votesRepository;

        public VotesService(
            IRepository<LakeVote> lakeVotesRepository,
            IRepository<TrophyVote> trophyVotesRepository,
            IRepository<Vote> votesRepository)
        {
            this.lakeVotesRepository = lakeVotesRepository;
            this.trophyVotesRepository = trophyVotesRepository;
            this.votesRepository = votesRepository;
        }

        public double GetLakeAverageVote(int lakeId)
        {
            var lakeVotes = this.lakeVotesRepository.All()
                .Where(lv => lv.LakeId == lakeId);

            if (lakeVotes.Count() == 0)
            {
                return 0;
            }

            return lakeVotes.Average(lv => lv.Vote.Value);
        }

        public int GetLakeRatersCount(int lakeId)
        {
            return this.lakeVotesRepository.All()
                .Where(lv => lv.LakeId == lakeId)
                .Count();
        }

        public int GetLakeRatersCountByValue(int lakeId, int value)
        {
            var lakeVotes = this.lakeVotesRepository.All()
                .Where(lv => lv.LakeId == lakeId);

            if (lakeVotes.Count() == 0)
            {
                return 0;
            }

            return this.lakeVotesRepository.All()
                .Where(lv => lv.Vote.Value == value)
                .Count();
        }

        public double GetTrophyAverageVote(int trophyId)
        {
            var trophyVotes = this.trophyVotesRepository.All()
               .Where(tv => tv.TrophyId == trophyId);

            if (trophyVotes.Count() == 0)
            {
                return 0;
            }

            return trophyVotes.Average(tv => tv.Vote.Value);
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
                .FirstOrDefault(lv => lv.LakeId == lakeId && lv.OwnerId == userId);

            if (lakeVote == null)
            {
                var vote = new Vote
                {
                    Value = value,
                };

                lakeVote = new LakeVote
                {
                    LakeId = lakeId,
                    OwnerId = userId,
                    Vote = vote,
                };

                await this.votesRepository.AddAsync(vote);
                await this.lakeVotesRepository.AddAsync(lakeVote);
            }
            else
            {
                var vote = this.votesRepository.All().Where(v => v.Id == lakeVote.VoteId).FirstOrDefault();
                vote.Value = value;
            }

            await this.votesRepository.SaveChangesAsync();
            await this.lakeVotesRepository.SaveChangesAsync();
        }

        public async Task SetTrophyVoteAsync(int trophyId, string userId, byte value)
        {
            var trophyVote = this.trophyVotesRepository.All()
                .FirstOrDefault(tv => tv.TrophyId == trophyId && tv.OwnerId == userId);

            if (trophyVote == null)
            {
                var vote = new Vote
                {
                    Value = value,
                };

                trophyVote = new TrophyVote
                {
                    TrophyId = trophyId,
                    OwnerId = userId,
                    Vote = vote,
                };

                await this.votesRepository.AddAsync(vote);
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
