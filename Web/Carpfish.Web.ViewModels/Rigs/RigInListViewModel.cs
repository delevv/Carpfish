namespace Carpfish.Web.ViewModels.Rigs
{
    using AutoMapper;
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class RigInListViewModel : IMapFrom<Rig>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int TrophiesCount { get; set; }

        public int StepsCount { get; set; }

        public int MaterialsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Rig, RigInListViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(r => r.Image.Url))
                .ForMember(x => x.TrophiesCount, opt =>
                  opt.MapFrom(r => r.Trophies.Count))
                .ForMember(x => x.StepsCount, opt =>
                  opt.MapFrom(r => r.Steps.Count))
                .ForMember(x => x.MaterialsCount, opt =>
                  opt.MapFrom(r => r.Materials.Count));
        }
    }
}
