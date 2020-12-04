namespace Carpfish.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Carpfish.Web.ViewModels.Trophies;

    public interface ITrophiesService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        Task AddAsync(AddTrophyInputModel input, string userId);

        T GetById<T>(int id);
    }
}
