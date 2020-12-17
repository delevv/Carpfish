namespace Carpfish.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Carpfish.Web.ViewModels.Trophies;

    public interface ITrophiesService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        Task AddAsync(AddTrophyInputModel input, string userId);

        IEnumerable<T> GetTopFiveBiggestTrophies<T>();

        T GetById<T>(int id);

        int GetCount();

        string GetTrophyOwnerId(int trophyId);

        Task UpdateAsync(int id, EditTrophyInputModel input);

        Task DeleteAsync(int id);
    }
}
