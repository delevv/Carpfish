﻿namespace Carpfish.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Carpfish.Web.ViewModels.Lakes;

    public interface ILakesService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        IEnumerable<T> GetAll<T>(LakesAllInputModel input);

        Task AddAsync(AddLakeInputModel input, string userId);

        T GetById<T>(int id);

        int GetCount();

        int GetCount(LakesAllInputModel input);

        string GetLakeOwnerId(int lakeId);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        Task UpdateAsync(int id, EditLakeInputModel input);

        Task DeleteAsync(int id);
    }
}
