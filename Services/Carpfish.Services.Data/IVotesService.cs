namespace Carpfish.Services.Data
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task SetLakeVoteAsync(int lakeId, string userId, byte value);

        Task SetTrophyVoteAsync(int trophyId, string userId, byte value);

        double GetLakeAverageVote(int lakeId);

        int GetLakeRatersCount(int lakeId);

        int GetLakeRatersCountByValue(int lakeId, int value);

        double GetTrophyAverageVote(int trophyId);

        int GetTrophyRatersCount(int trophyId);
    }
}
