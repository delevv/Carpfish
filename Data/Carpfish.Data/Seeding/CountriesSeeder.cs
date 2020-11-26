namespace Carpfish.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpfish.Data.Models;

    public class CountriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Countries.Any())
            {
                return;
            }

            var countries = new List<Country>();

            using (StreamReader r = new StreamReader(@"../../Data/Carpfish.Data/Seeding/countries-list.txt"))
            {
                countries = r.ReadToEnd()
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => new Country
                    {
                        Name = c,
                    })
                    .ToList();
            }

            await dbContext.Countries.AddRangeAsync(countries);
        }
    }
}
