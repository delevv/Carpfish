using Carpfish.Data.Models;
using Carpfish.Services.Mapping;

public class LakeModel : IMapFrom<Lake>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int CountryId { get; set; }

    public string WebsiteUrl { get; set; }

    public double Area { get; set; }

    public bool IsFree { get; set; }
}
