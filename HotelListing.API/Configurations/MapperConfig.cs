using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.Models.Country;
using HotelListing.API.Models.Hotel;
using HotelListing.API.Models.Users;

namespace HotelListing.API.Configurations
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
