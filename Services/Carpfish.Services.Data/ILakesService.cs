namespace Carpfish.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Carpfish.Web.ViewModels.Lakes;

    public interface ILakesService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task AddAsync(AddLakeInputModel input, string userId);
    }
}
