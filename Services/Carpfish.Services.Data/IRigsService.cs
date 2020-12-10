namespace Carpfish.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Carpfish.Web.ViewModels.Rigs;

    public interface IRigsService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        Task AddAsync(AddRigInputModel input, string userId);

        T GetById<T>(int id);
    }
}
