namespace Carpfish.Web.ViewModels.Lakes
{
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class EditLakeInputModel : BaseLakeInputModel, IMapFrom<Lake>
    {
        public int Id { get; set; }
    }
}
