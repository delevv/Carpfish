namespace Carpfish.Web.ViewModels.Lakes
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class LakeByIdViewModel : IMapFrom<Lake>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string OwnerUserName { get; set; }

        public double Area { get; set; }

        public string CountryName { get; set; }

        public string Coordinates { get; set; }

        public string WebsiteUrl { get; set; }

        public string IsFree { get; set; }

        public string MainImg { get; set; }

        public string[] OtherImages { get; set; }

        [IgnoreMap]
        public LakeRatingViewModel Rating { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Lake, LakeByIdViewModel>()
               .ForMember(x => x.MainImg, opt =>
                 opt.MapFrom(l => l.LakeImages
                                     .FirstOrDefault(li => li.IsMain)
                                     .Image.Url))
               .ForMember(x => x.IsFree, opt =>
                   opt.MapFrom(l => l.IsFree ? "Free" : "Paid"))
               .ForMember(x => x.OtherImages, opt =>
                 opt.MapFrom(l => l.LakeImages.Where(i => !i.IsMain).Select(li => li.Image.Url)));
        }
    }
}
