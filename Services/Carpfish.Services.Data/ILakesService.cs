namespace Carpfish.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILakesService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<string> AddAsync();
    }
}
