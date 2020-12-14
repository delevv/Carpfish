namespace Carpfish.Web.ViewModels.Steps
{
    using AutoMapper;
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class StepInRigViewModel : IMapFrom<Step>, IHaveCustomMappings
    {
        public int StepNumber { get; set; }

        public string ImageUrl { get; set; }

        public string LessDescription { get; set; }

        public string MoreDescription { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Step, StepInRigViewModel>()
                .ForMember(x => x.LessDescription, opt =>
                  opt.MapFrom(s => s.Description.Length <= 300
                  ? s.Description
                  : s.Description.Substring(0, 300)))
                .ForMember(x => x.MoreDescription, opt =>
                  opt.MapFrom(s => s.Description.Length <= 300
                  ? string.Empty
                  : s.Description.Substring(300)));
        }
    }
}
