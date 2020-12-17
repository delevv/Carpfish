namespace Carpfish.Web.ViewModels.Rigs
{
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class EditRigInputModel : BaseRigInputModel, IMapFrom<Rig>
    {
        public int Id { get; set; }
    }
}
