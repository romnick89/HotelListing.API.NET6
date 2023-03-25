using HotelListing.API.Models.Hotel;
using Microsoft.Build.Evaluation;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.API.Models.Country
{
    public class GetCountryModel : BaseCountryModel
    {
        public int Id { get; set; }
    }
}
