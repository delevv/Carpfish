namespace Carpfish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpfish.Common;
    using Carpfish.Data.Common.Repositories;
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;
    using Carpfish.Web.ViewModels.Lakes;

    public class LakesService : ILakesService
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly IDeletableEntityRepository<Lake> lakeRepository;

        public LakesService(
            ICloudinaryService cloudinaryService,
            IDeletableEntityRepository<Lake> lakeRepository)
        {
            this.cloudinaryService = cloudinaryService;
            this.lakeRepository = lakeRepository;
        }

        public async Task AddAsync(AddLakeInputModel input, string userId)
        {
            var lake = new Lake()
            {
                Name = input.Name,
                OwnerId = userId,
                CountryId = input.CountryId,
                Area = input.Area,
                IsFree = input.IsFree,
                WebsiteUrl = input.WebsiteUrl,
            };

            lake.Location = new Location
            {
                Latitude = input.Location.Latitude,
                Longitude = input.Location.Longitude,
            };

            var mainImgUrl = await this.cloudinaryService.UploadAsync(input.MainImage, input.MainImage.FileName);

            var mainImg = new Image()
            {
                OwnerId = userId,
                Url = mainImgUrl,
            };

            lake.LakeImages.Add(new LakeImage()
            {
                Image = mainImg,
                IsMain = true,
            });

            if (input.OtherImages != null)
            {
                foreach (var img in input.OtherImages)
                {
                    var currImgUrl = await this.cloudinaryService.UploadAsync(img, img.FileName);

                    var currImg = new Image()
                    {
                        OwnerId = userId,
                        Url = currImgUrl,
                    };

                    lake.LakeImages.Add(new LakeImage()
                    {
                        Image = currImg,
                    });
                }
            }

            await this.lakeRepository.AddAsync(lake);
            await this.lakeRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lake = this.lakeRepository.All()
                .FirstOrDefault(l => l.Id == id);

            this.lakeRepository.Delete(lake);
            await this.lakeRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.lakeRepository.All()
                 .OrderByDescending(l => l.Id)
                 .To<T>()
                 .ToList();
        }

        public IEnumerable<T> GetAll<T>(LakesAllInputModel input)
        {
            var query = this.lakeRepository.All().AsQueryable();

            switch (input.Type)
            {
                case "Free":
                    query = query.Where(l => l.IsFree);
                    break;
                case "Paid":
                    query = query.Where(l => !l.IsFree);
                    break;
            }

            switch (input.Order)
            {
                case "Rating ASC":
                    query = query.OrderBy(x => x.LakeVotes.Average(lv => lv.Vote.Value));
                    break;
                case "Rating DESC":
                    query = query.OrderByDescending(x => x.LakeVotes.Average(lv => lv.Vote.Value));
                    break;
                case "Trophies Count ASC":
                    query = query.OrderBy(x => x.Trophies.Count());
                    break;
                case "Trophies Count DESC":
                    query = query.OrderByDescending(x => x.Trophies.Count());
                    break;
            }

            if (!string.IsNullOrEmpty(input.Search))
            {
                query = query.Where(l => l.Name.ToLower().Contains(input.Search.ToLower()));
            }

            return query
                    .Skip((input.Page - 1) * GlobalConstants.LakesCountPerPage)
                    .Take(GlobalConstants.LakesCountPerPage)
                    .To<T>()
                    .ToList();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.lakeRepository
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

        public T GetById<T>(int id)
        {
            return this.lakeRepository.All()
                .Where(l => l.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public int GetCount()
        {
            return this.lakeRepository.All().Count();
        }

        public int GetFreeCount()
        {
            return this.lakeRepository
                .AllAsNoTracking()
                .Where(l => l.IsFree)
                .Count();
        }

        public string GetLakeOwnerId(int lakeId)
        {
            return this.lakeRepository.All()
                .FirstOrDefault(l => l.Id == lakeId)
                .OwnerId;
        }

        public int GetPaidCount()
        {
            return this.lakeRepository
                .AllAsNoTracking()
                .Where(l => !l.IsFree)
                .Count();
        }

        public int GetSearchCount(string type, string search)
        {
            return type switch
            {
                "Free" => this.lakeRepository.AllAsNoTracking().Where(l => l.IsFree && l.Name.ToLower().Contains(search.ToLower())).Count(),
                "Paid" => this.lakeRepository.AllAsNoTracking().Where(l => !l.IsFree && l.Name.ToLower().Contains(search.ToLower())).Count(),
                _ => this.lakeRepository.AllAsNoTracking().Where(l => l.Name.ToLower().Contains(search.ToLower())).Count(),
            };
        }

        public async Task UpdateAsync(int id, EditLakeInputModel input)
        {
            var lake = this.lakeRepository.All()
                .FirstOrDefault(r => r.Id == id);

            lake.IsFree = input.IsFree;
            lake.Name = input.Name;
            lake.WebsiteUrl = input.WebsiteUrl;
            lake.Area = input.Area;
            lake.CountryId = input.CountryId;

            await this.lakeRepository.SaveChangesAsync();
        }
    }
}
