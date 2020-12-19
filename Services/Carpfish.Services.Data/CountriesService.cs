namespace Carpfish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Carpfish.Data.Common.Repositories;
    using Carpfish.Data.Models;

    public class CountriesService : ICountriesService
    {
        private readonly IRepository<Country> countriesRepository;

        public CountriesService(IRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.countriesRepository
                .All()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
