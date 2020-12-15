namespace Carpfish.Web.ViewModels.Lakes
{
    using AutoMapper;
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class LakeInIndexMapViewModel : IMapFrom<Lake>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double LocationLatitude { get; set; }

        public double LocationLongitude { get; set; }

        public string PaidOrFree { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Lake, LakeInIndexMapViewModel>()
                .ForMember(x => x.PaidOrFree, opt =>
                  opt.MapFrom(l => l.IsFree ? "Free" : "Paid"));
        }
    }
}
