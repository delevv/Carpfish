namespace Carpfish.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;
    using Carpfish.Data.Common.Models;

    public class Location : BaseModel<int>
    {
        [Required]
        [Range(GlobalConstants.LocationLatitudeMinValue, GlobalConstants.LocationLatitudeMaxValue)]
        public double Latitude { get; set; }

        [Required]
        [Range(GlobalConstants.LocationLongitudeMinValue, GlobalConstants.LocationLongitudeMaxValue)]
        public double Longitude { get; set; }

        public int LakeId { get; set; }

        [Required]
        public virtual Lake Lake { get; set; }
    }
}
