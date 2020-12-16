namespace Carpfish.Services.Data
{
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

            for (int i = 0; i < input.Materials.Count(); i++)
            {
                rig.Materials.Add(new Material
                {
                    Rig = rig,
                    Description = input.Materials[i].Description,
                    MaterialNumber = i + 1,
                });
            }

            for (int i = 0; i < input.Steps.Count(); i++)
            {
                // TODO: Validate Image
                var stepImgUrl = await this.cloudinaryService.UploadAsync(input.Steps[i].Image, input.Steps[i].Image.FileName);

                rig.Steps.Add(new Step
                {
                    Rig = rig,
                    Description = input.Steps[i].Description,
                    StepNumber = i + 1,
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

        public int GetCount()
        {
            return this.rigsRepository.All().Count();
        }

        public IEnumerable<T> GetFiveRigsWithMostTrophies<T>()
        {
            return this.rigsRepository.All()
                 .OrderByDescending(r => r.Trophies.Count)
                 .Take(5)
                 .To<T>()
                 .ToList();
        }
    }
}
