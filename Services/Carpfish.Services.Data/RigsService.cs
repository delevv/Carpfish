namespace Carpfish.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpfish.Data.Common.Repositories;
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;
    using Carpfish.Web.ViewModels.Rigs;

    public class RigsService : IRigsService
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly IDeletableEntityRepository<Rig> rigsRepository;

        public RigsService(
            ICloudinaryService cloudinaryService,
            IDeletableEntityRepository<Rig> rigsRepository)
        {
            this.cloudinaryService = cloudinaryService;
            this.rigsRepository = rigsRepository;
        }

        public async Task AddAsync(AddRigInputModel input, string userId)
        {
            var rig = new Rig
            {
                Name = input.Name,
                Description = input.Description,
                OwnerId = userId,
            };

            // TODO: Validate Image
            var rigImgUrl = await this.cloudinaryService.UploadAsync(input.Image, input.Image.FileName);

            rig.Image = new Image
            {
                OwnerId = userId,
                Url = rigImgUrl,
            };

            foreach (var material in input.Materials)
            {
                rig.Materials.Add(new Material
                {
                    Rig = rig,
                    Description = material.Description,
                    MaterialNumber = material.MaterialNumber,
                });
            }

            foreach (var step in input.Steps)
            {
                // TODO: Validate Image
                var stepImgUrl = await this.cloudinaryService.UploadAsync(step.Image, step.Image.FileName);

                rig.Steps.Add(new Step
                {
                    Rig = rig,
                    Description = step.Description,
                    StepNumber = step.StepNumber,
                    Image = new Image
                    {
                        OwnerId = userId,
                        Url = stepImgUrl,
                    },
                });
            }

            await this.rigsRepository.AddAsync(rig);
            await this.rigsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            return this.rigsRepository.AllAsNoTracking()
                .OrderByDescending(r => r.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.rigsRepository.All()
                 .Where(r => r.Id == id)
                 .To<T>()
                 .FirstOrDefault();
        }
    }
}
