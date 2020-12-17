namespace Carpfish.Web.ViewModels.Rigs
{
    using System.Collections.Generic;

    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;
    using Carpfish.Web.ViewModels.Materials;
    using Carpfish.Web.ViewModels.Steps;
    using Carpfish.Web.ViewModels.Trophies;

    public class RigByIdViewModel : IMapFrom<Rig>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string OwnerId { get; set; }

        public string OwnerUserName { get; set; }

        public List<MaterialInRigViewModel> Materials { get; set; }

        public List<StepInRigViewModel> Steps { get; set; }

        public List<TrophyInRigViewModel> Trophies { get; set; }
    }
}
