namespace Carpfish.Web.ViewModels.Trophies
{
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class EditTrophyInputModel : BaseTrophyInputModel, IMapFrom<Trophy>
    {
        public int Id { get; set; }
    }
}
