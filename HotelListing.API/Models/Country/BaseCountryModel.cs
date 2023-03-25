using Microsoft.Build.Framework;

namespace HotelListing.API.Models.Country
{
    public abstract class BaseCountryModel
    {
        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
