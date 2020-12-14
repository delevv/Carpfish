namespace Carpfish.Web.ViewModels.Materials
{
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class MaterialInRigViewModel : IMapFrom<Material>
    {
        public int MaterialNumber { get; set; }

        public string Description { get; set; }
    }
}
