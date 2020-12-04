namespace Carpfish.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpfish.Data.Common.Repositories;
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;
    using Carpfish.Web.ViewModels.Lakes;

    public class LakesService : ILakesService
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly IDeletableEntityRepository<Lake> lakeRepository;
        private readonly IDeletableEntityRepository<LakeImage> lakeImagesRepository;

        public LakesService(
            ICloudinaryService cloudinaryService,
            IDeletableEntityRepository<Lake> lakeRepository,
            IDeletableEntityRepository<LakeImage> lakeImagesRepository)
        {
            this.cloudinaryService = cloudinaryService;
            this.lakeRepository = lakeRepository;
            this.lakeImagesRepository = lakeImagesRepository;
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
                Coordinates = input.Coordinates,
                WebsiteUrl = input.WebsiteUrl,
            };

            // :TODO Validate Image
            var mainImgUrl = await this.cloudinaryService.UploadAsync(input.MainImage, input.MainImage.FileName);

            var mainImg = new Image()
            {
                OwnerId = userId,
                Url = mainImgUrl,
            };

            await this.lakeImagesRepository.AddAsync(new LakeImage()
            {
                Image = mainImg,
                Lake = lake,
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

                    await this.lakeImagesRepository.AddAsync(new LakeImage()
                    {
                        Image = currImg,
                        Lake = lake,
                    });
                }
            }

            await this.lakeRepository.AddAsync(lake);

            await this.lakeRepository.SaveChangesAsync();
            await this.lakeImagesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            return this.lakeRepository.AllAsNoTracking()
                 .OrderByDescending(l => l.Id)
                 .Skip((page - 1) * itemsPerPage)
                 .Take(itemsPerPage)
                 .To<T>()
                 .ToList();
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
    }
}
