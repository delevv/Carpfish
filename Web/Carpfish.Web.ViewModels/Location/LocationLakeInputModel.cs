namespace Carpfish.Web.ViewModels.Location
{
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;

    public class LocationLakeInputModel
    {
        [Required]
        [Range(GlobalConstants.LocationLatitudeMinValue, GlobalConstants.LocationLatitudeMaxValue)]
        public double Latitude { get; set; }

        [Required]
        [Range(GlobalConstants.LocationLongitudeMinValue, GlobalConstants.LocationLongitudeMaxValue)]
        public double Longitude { get; set; }
    }
}
