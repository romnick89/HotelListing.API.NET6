using Microsoft.Build.Framework;

namespace HotelListing.API.Core.Models.Country
{
    public abstract class BaseCountryModel
    {
        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
