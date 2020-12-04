namespace Carpfish.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Carpfish.Web.ViewModels.Trophies;

    public class TrophiesService : ITrophiesService
    {
        public Task AddAsync(AddTrophyInputModel input, string userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            throw new System.NotImplementedException();
        }

        public T GetById<T>(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
