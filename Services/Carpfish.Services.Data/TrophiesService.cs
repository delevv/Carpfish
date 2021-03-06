﻿namespace Carpfish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpfish.Data.Common.Repositories;
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;
    using Carpfish.Web.ViewModels.Trophies;

    public class TrophiesService : ITrophiesService
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly IDeletableEntityRepository<Trophy> trophiesRepository;

        public TrophiesService(
            ICloudinaryService cloudinaryService,
            IDeletableEntityRepository<Trophy> trophiesRepository)
        {
            this.cloudinaryService = cloudinaryService;
            this.trophiesRepository = trophiesRepository;
        }

        public async Task AddAsync(AddTrophyInputModel input, string userId)
        {
            var trophy = new Trophy()
            {
                Weight = input.Weight,
                BaitDescription = input.BaitDescription,
                LakeId = input.LakeId,
                RigId = input.RigId,
                OwnerId = userId,
            };

            var mainImgUrl = await this.cloudinaryService.UploadAsync(input.MainImage, input.MainImage.FileName);

            var mainImg = new Image()
            {
                OwnerId = userId,
                Url = mainImgUrl,
            };

            trophy.TrophyImages.Add(new TrophyImage
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

                    trophy.TrophyImages.Add(new TrophyImage()
                    {
                        Image = currImg,
                    });
                }
            }

            await this.trophiesRepository.AddAsync(trophy);
            await this.trophiesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var trophy = this.trophiesRepository.All()
                 .FirstOrDefault(t => t.Id == id);

            this.trophiesRepository.Delete(trophy);
            await this.trophiesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            return this.trophiesRepository.All()
                .OrderByDescending(l => l.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.trophiesRepository.All()
                 .Where(t => t.Id == id)
                 .To<T>()
                 .FirstOrDefault();
        }

        public int GetCount()
        {
            return this.trophiesRepository.All().Count();
        }

        public IEnumerable<T> GetTopFiveBiggestTrophies<T>()
        {
            return this.trophiesRepository.All()
                .OrderByDescending(t => t.Weight)
                .Take(5)
                .To<T>()
                .ToList();
        }

        public string GetTrophyOwnerId(int trophyId)
        {
            return this.trophiesRepository.All()
                .FirstOrDefault(t => t.Id == trophyId)
                .OwnerId;
        }

        public async Task UpdateAsync(int id, EditTrophyInputModel input)
        {
            var trophy = this.trophiesRepository.All()
                 .FirstOrDefault(r => r.Id == id);

            trophy.LakeId = input.LakeId;
            trophy.RigId = input.RigId;
            trophy.BaitDescription = input.BaitDescription;
            trophy.Weight = input.Weight;

            await this.trophiesRepository.SaveChangesAsync();
        }
    }
}
