namespace Carpfish.Web.ViewModels.Lakes
{
    using System.Linq;

    using AutoMapper;
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class LakeInListViewModel : IMapFrom<Lake>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public string CountryName { get; set; }

        public string IsFree { get; set; }

        public double Rating { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Lake, LakeInListViewModel>()
                .ForMember(x => x.ImgUrl, opt =>
                  opt.MapFrom(l => l.LakeImages
                                      .FirstOrDefault(li => li.IsMain)
                                      .Image.Url))
                .ForMember(x => x.IsFree, opt =>
                    opt.MapFrom(l => l.IsFree ? "Free" : "Paid"));
        }
    }
}
