namespace Carpfish.Services.Data.Tests.TestModels
{
    using System.Collections.Generic;

    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class RigModel : IMapFrom<Rig>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Trophy> Trophies { get; set; }
    }
}
