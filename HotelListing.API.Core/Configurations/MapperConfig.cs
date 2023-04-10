using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.Core.Models.Country;
using HotelListing.API.Core.Models.Hotel;
using HotelListing.API.Core.Models.Users;

namespace HotelListing.API.Core.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, CreateCountryModel>().ReverseMap();
            CreateMap<Country, GetCountryModel>().ReverseMap();
            CreateMap<Country, CountryModel>().ReverseMap();
            CreateMap<Country, UpdateCountryModel>().ReverseMap();

            CreateMap<Hotel, HotelModel>().ReverseMap();
            CreateMap<Hotel, CreateHotelModel>().ReverseMap();
            CreateMap<APIUserModel, APIUser>().ReverseMap();
        }
    }
}
