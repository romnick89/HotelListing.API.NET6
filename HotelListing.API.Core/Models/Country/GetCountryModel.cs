using HotelListing.API.Core.Models.Hotel;
using Microsoft.Build.Evaluation;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.API.Core.Models.Country
{
    public class GetCountryModel : BaseCountryModel
    {
        public int Id { get; set; }
    }
}
