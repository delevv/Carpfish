﻿namespace Carpfish.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Carpfish.Web.ViewModels.Rigs;

    public interface IRigsService
    {
        IEnumerable<T> GetAll<T>(RigsAllInputModel input);

        Task AddAsync(AddRigInputModel input, string userId);

        T GetById<T>(int id);

        IEnumerable<T> GetFiveRigsWithMostTrophies<T>();

        int GetCount();

        int GetSearchCount(string search);

        string GetRigOwnerId(int rigId);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        Task UpdateAsync(int id, EditRigInputModel input);

        Task DeleteAsync(int id);
    }
}
